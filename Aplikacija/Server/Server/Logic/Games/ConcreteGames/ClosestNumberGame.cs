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
        private int numOfQuestions;
        private int currQuestion;
        private int playerCount;
        private IBaseRepository<Question> _baseQuestionRepository;

        public ClosestNumberGame(Match match)
        {
            _match = match;
        }

        public ClosestNumberGame(IBaseRepository<Question> baseQuestionRepository, Match match)
        {
            _baseQuestionRepository = baseQuestionRepository;
            _match = match;
            numOfQuestions = 5;
            currQuestion = -1;
            questions = _baseQuestionRepository.Sample<ClosestNumber>(numOfQuestions);
            timer = new Timer(1000);
            timer.AutoReset = true;
            timer.Elapsed += Tick;
            timer.Enabled = true;
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
            for (int i = 0; i < playerCount; i++)
            {
                answers[i] = null;
                state.CanAnswer[i] = true;
            }
        }

        private void Tick(Object source, ElapsedEventArgs e)
        {
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
            state.Active = false;
            state.Question = questions[currQuestion].Text;
            state.Answer = questions[currQuestion].Answer;
            int winner = -1;
            for(int i=0;i<playerCount;i++)
            {
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
            nextTimer = new Timer(3000);
            nextTimer.AutoReset = false;
            nextTimer.Elapsed += Next;
            nextTimer.Enabled = true;
            _match.SendUpdate(GetState());
        }
        private void Next(Object source, ElapsedEventArgs e)
        {
            nextTimer.Stop();
            nextTimer.Dispose();
            NextQuestion();
        }

        private void NextQuestion()
        {
            timer.Stop();
            timer.Dispose();
            currQuestion++;
            if(currQuestion==numOfQuestions)
            {
                EndGame();
                return;
            }
            ResetAnswers();
            state.Question = questions[currQuestion].Text;
            state.Active = true;
            timerValue = timePerQuestion;
            timer = new Timer(1000);
            timer.AutoReset = true;
            timer.Elapsed += Tick;
            timer.Enabled = true;
            _match.SendUpdate(GetState());
        }

        private void EndGame()
        {
            timer.Stop();
            timer.Dispose();
            nextTimer.Stop();
            nextTimer.Dispose();
            _match.Players = state.Players;
            _match.StartNextGame();
        }

        public GameState GetState()
        {
            state.TimerValue = timerValue;
            return state;
        }

        public void SubmitAnswer(Answer answer, string username)
        {
            if (state.Active)
            {
                bool allAnswered = true;
                for (int i = 0; i < playerCount; i++)
                {
                    if (username == state.Players[i].User.Username && state.CanAnswer[i] == true)
                    {
                        answers[i] = float.Parse(answer.Text);
                        state.CanAnswer[i] = false;
                    }
                    if (state.CanAnswer[i] == true)
                        allAnswered = false;
                }
                if (allAnswered)
                    ShowAnswer();
            }
        }
    }
}
