using Microsoft.Extensions.DependencyInjection;
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
    public class ClosestNumberGame : IGame
    {
        private List<ClosestNumber> questions;
        private float?[] answers;
        private ClosestNumberState state;
        private Match _match;
        private int timePerQuestion = 20;
        private int timerValue;
        private Timer timer;
        private Timer nextTimer;
        private int numOfQuestions=5;
        private int currQuestion;
        private int playerCount;
        private IBaseRepository<Question> _baseQuestionRepository;
        private IServiceScope scope;

        public ClosestNumberGame(Match match)
        {
            _match = match;
        }

        public ClosestNumberGame(Match match, IServiceScopeFactory serviceScopeFactory)
        {
            scope = serviceScopeFactory.CreateScope();
            _baseQuestionRepository = scope.ServiceProvider.GetService<IBaseRepository<Question>>();
            _match = match;
            currQuestion = -1;
            state = new ClosestNumberState(_match.Players);
            questions = _baseQuestionRepository.Sample<ClosestNumber>(numOfQuestions);
            timer = new Timer(1000);
            timer.AutoReset = true;
            timer.Elapsed += Tick;

            nextTimer = new Timer(5000);
            nextTimer.AutoReset = false;
            nextTimer.Elapsed += Next;
            StartGame();
        }

        private void StartGame()
        {
            playerCount = _match.Players.Count();
            answers = new float?[playerCount];
            ResetAnswers();
            state = new ClosestNumberState(_match.Players);
            NextQuestion();
        }

        private void ResetAnswers()
        {
            state.Answer = null;
            for (int i = 0; i < playerCount; i++)
            {
                answers[i] = null;
                state.Answers[i] = null;
                state.CanAnswer[i] = true;
            }
        }

        private void Tick(Object source, ElapsedEventArgs e)
        {
            _match.Tick();
            timerValue--;
            if (timerValue <= 0)
            {
                timer.Stop();
                ShowAnswer();
            }
        }

        private void ShowAnswer()
        {
            timer.Stop();
            nextTimer.Stop();
            state.IsActive = false;
            state.Question = questions[currQuestion].Text;
            state.Answer = questions[currQuestion].Answer;
            int winner = -1;
            for(int i=0;i<playerCount;i++)
            {
                state.Answers[i] = answers[i];
                state.IsWinner[i] = false;
                if (answers[i] != null)
                {
                    if (winner == -1)
                        winner = i;
                    else
                    {
                        if(Math.Abs((decimal)(answers[winner]-questions[currQuestion].Answer)) >= Math.Abs((decimal)(answers[i] - questions[currQuestion].Answer)))
                            winner = i;
                    }
                }
            }
            for (int i = 0; i < playerCount; i++)
            {
                if (winner != -1)
                {
                    if(answers[winner]==answers[i])
                    {
                        state.IsWinner[i] = true;
                        state.Players[i].Points += questions[currQuestion].Points;
                        if(answers[winner]== questions[currQuestion].Answer)
                            state.Players[i].Points += questions[currQuestion].Points/2;
                    }
                }
            }
            nextTimer.Start();
            _match.SendUpdate(GetState());
        }
        private void Next(Object source, ElapsedEventArgs e)
        {
            nextTimer.Stop();
            NextQuestion();
        }

        private void NextQuestion()
        {
            timer.Stop();
            currQuestion++;
            if(currQuestion==numOfQuestions)
            {
                EndGame();
                return;
            }
            ResetAnswers();
            state.Question = questions[currQuestion].Text;
            state.IsActive = true;
            timerValue = timePerQuestion;
            timer.Start();
            _match.SendUpdate(GetState());
        }

        private void EndGame()
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
            if (state.IsActive)
            {
                bool allAnswered = true;
                for (int i = 0; i < playerCount; i++)
                {
                    if (username == state.Players[i].User.Username && state.CanAnswer[i] == true)
                    {
                        answers[i] = float.Parse(answer.Text);
                        state.CanAnswer[i] = false;
                    }
                    if (state.CanAnswer[i])
                        allAnswered = false;
                }
                if (allAnswered)
                    ShowAnswer();
            }
        }
        public void FlagConnected(string username)
        {
            Player p = state.Players.Where(x => x.User.Username == username).FirstOrDefault();
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
            nextTimer.Stop();
            nextTimer.Dispose();
        }
    }
}
