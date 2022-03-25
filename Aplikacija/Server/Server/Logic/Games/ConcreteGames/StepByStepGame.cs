using Server.Logic.DTOs;
using Server.Logic.DTOs.ConcreteGameStates;
using Server.Logic.Masters.Match;
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
        private Match _match;
        private StepByStep stepByStep;
        private StepByStepState state;
        private int step;
        private string[] answers;
        private int playerCount;
        private int perStep = 15;
        private int betweenSteps = 3;
        private int timerValue;
        private Timer timer;
        private int betweenTimerValue;
        private Timer betweenTimer;
        private Timer endTimer;

        public StepByStepGame(IBaseRepository<Question> baseQuestionRepository, Match match)
        {
            _baseQuestionRepository = baseQuestionRepository;
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
            NextStep();
        }
        private void Tick(Object source, ElapsedEventArgs e)
        {
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

            betweenTimerValue = betweenSteps*1000;
            betweenTimer = new Timer(betweenTimerValue);
            betweenTimer.AutoReset = false;
            betweenTimer.Elapsed += Next;
            betweenTimer.Enabled = true;
        }
        private void Next(Object source, ElapsedEventArgs e)
        {
            betweenTimer.Stop();
            betweenTimer.Dispose();
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

            _match.SendUpdate(GetState());

            timerValue = perStep;
            timer.Start();
        }

        private void ResetAnswers()
        {
            for(int i=0;i<playerCount;i++)
            {
                state.Answers[i] = "";
                state.CanAnswer[i] = true;
            }
        }

        private void EndGame()
        {
            state.IsActive = false;
            state.FinalAnswer = stepByStep.Answer;
            _match.SendUpdate(GetState());
            endTimer = new Timer(3000);
            endTimer.AutoReset = false;
            endTimer.Elapsed += GameEnded;
            endTimer.Enabled = true;
        }
        private void GameEnded(Object source, ElapsedEventArgs e)
        {
            timer.Stop();
            timer.Dispose();
            betweenTimer.Stop();
            betweenTimer.Dispose();
            endTimer.Stop();
            endTimer.Dispose();
            _match.StartNextGame();
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
                    if(answer.Text==stepByStep.Answer)
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
    }
}
