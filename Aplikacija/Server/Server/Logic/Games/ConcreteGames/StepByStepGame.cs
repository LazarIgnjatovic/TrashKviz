using Microsoft.Extensions.DependencyInjection;
using Server.Logic.DTOs;
using Server.Logic.DTOs.ConcreteGameStates;
using Server.Logic.Masters.Match;
using Server.Logic.Services;
using Server.Persistence.Models;
using Server.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Server.Logic.Games.ConcreteGames
{
    public class StepByStepGame : IGame
    {
        private IBaseRepository<Question> _baseQuestionRepository;
        private IStandardStringService _standardStringService;
        private IServiceScope scope;
        private Match _match;
        private StepByStep stepByStep;
        private StepByStepState state;
        private int step;
        private string[] answers;
        private int playerCount;
        private int perStep = 20;
        private int betweenSteps = 3;
        private int timerValue;
        private Timer timer;
        private int betweenTimerValue;
        private Timer betweenTimer;
        private Timer endTimer;

        public StepByStepGame(Match match, IServiceScopeFactory serviceScopeFactory)
        {
            scope = serviceScopeFactory.CreateScope();
            _baseQuestionRepository = scope.ServiceProvider.GetService<IBaseRepository<Question>>();
            _standardStringService = scope.ServiceProvider.GetService<IStandardStringService>();
            _match = match;
            stepByStep = _baseQuestionRepository.Sample<StepByStep>(1)[0];
            StartGame();
        }

        private void StartGame()
        {
            step = -1;
            playerCount = _match.Players.Count();
            answers = new string[playerCount];
            state = new StepByStepState(_match.Players,stepByStep.Steps.Length);

            timerValue = perStep;
            timer = new Timer(1000);
            timer.AutoReset = true;
            timer.Elapsed += Tick;
            timer.Enabled = true;

            betweenTimerValue = betweenSteps * 1000;
            betweenTimer = new Timer(betweenTimerValue);
            betweenTimer.AutoReset = false;
            betweenTimer.Elapsed += Next;

            endTimer = new Timer(3000);
            endTimer.AutoReset = false;
            endTimer.Elapsed += GameEnded;

            NextStep();
        }
        private void Tick(Object source, ElapsedEventArgs e)
        {
            _match.Tick();
            timerValue--;
            if (timerValue <= 0)
            {
                timer.Stop();
                ShowAnswers();
            }
        }

        private void ShowAnswers()
        {
            timer.Stop();
            state.Answers = answers;
            state.IsActive = false;
            _match.SendUpdate(state);
            betweenTimer.Start();
        }
        private void Next(Object source, ElapsedEventArgs e)
        {
            betweenTimer.Stop();
            NextStep();
        }

        private void NextStep()
        {
            timer.Stop();
            step++;
            if(step>=stepByStep.Steps.Count())
            {
                EndGame();
                return;
            }
            state.IsActive = true;
            state.Steps[step] = stepByStep.Steps[step];
            ResetAnswers();
            timerValue = perStep;
            _match.SendUpdate(GetState());
            timer.Start();
        }

        private void ResetAnswers()
        {
            for(int i=0;i<playerCount;i++)
            {
                state.Answers[i] = null;
                state.CanAnswer[i] = true;
            }
        }

        private void EndGame()
        {
            state.IsActive = false;
            for(int i=0;i<stepByStep.Steps.Length;i++)
            {
                state.Steps[i] = stepByStep.Steps[i];
            }
            state.FinalAnswer = stepByStep.Answer;
            _match.SendUpdate(GetState());

            endTimer.Start();
        }
        private void GameEnded(Object source, ElapsedEventArgs e)
        {
            Quit();
            _match.Players = state.Players;
            _match.GameEnded();
        }

        public GameState GetState()
        {
            state.TimerValue = timerValue;
            return state;
        }
        public void SubmitAnswer(Answer answer, string username)
        {
            bool allAnswered = true;
            for(int i=0;i<playerCount;i++)
            {
                if(state.Players[i].User.Username==username && state.CanAnswer[i])
                {
                    state.CanAnswer[i] = false;
                    state.Answers[i] = answer.Text;
                    if(_standardStringService.Standardize(answer.Text)== _standardStringService.Standardize(stepByStep.Answer))
                    {
                        state.Players[i].Points += (stepByStep.Steps.Length-step)*stepByStep.Points/3;
                        state.Winner = i;
                        EndGame();
                        return;
                    }
                }
                if (state.CanAnswer[i])
                    allAnswered = false;
            }
            if (allAnswered)
                ShowAnswers();
        }

        public void FlagConnected(string username)
        {
            Player p=state.Players.Where(x => x.User.Username == username).FirstOrDefault();
            if (p != null)
                p.IsConnected = true;
        }

        public void FlagDisconnected(string username)
        {
            Player p = state.Players.Where(x => x.User.Username == username).FirstOrDefault();
            if (p != null)
                p.IsConnected = false;
        }

        public void Quit()
        {
            timer.Stop();
            timer.Dispose();
            betweenTimer.Stop();
            betweenTimer.Dispose();
            endTimer.Stop();
            endTimer.Dispose();
        }
    }
}
