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
    public class MultipleChoiceGame : IGame
    {
        private List<MultipleChoice> questions;
        private Timer timer;
        private Timer nextTimer;
        private MultipleChoiceState state;
        private int?[] answers;
        private int numOfQuestions=5;
        private int currQuestion;
        private int playerCount;
        private int timePerQuestion = 20;
        private int timerValue;
        private int? first;
        private int? second;
        private Match _match;
        private IBaseRepository<Question> _baseQuestionRepository;
        private IServiceScope scope;

        public MultipleChoiceGame(Match match, IServiceScopeFactory serviceScopeFactory)
        {
            scope = serviceScopeFactory.CreateScope();
            _baseQuestionRepository = scope.ServiceProvider.GetService<IBaseRepository<Question>>();
            _match = match;
            currQuestion = -1;
            questions= _baseQuestionRepository.Sample<MultipleChoice>(numOfQuestions);
            playerCount = _match.Players.Count();
            answers = new int?[playerCount];
            state = new MultipleChoiceState(_match.Players);

            timer = new Timer(1000);
            timer.AutoReset = true;
            timer.Elapsed += Tick;

            nextTimer = new Timer(3000);
            nextTimer.AutoReset = false;
            nextTimer.Elapsed += Next;
            StartGame();
        }

        private void StartGame()
        {
            NextQuestion();
        }

        private void NextQuestion()
        {
            timer.Stop();
            currQuestion++;
            if (currQuestion == numOfQuestions)
            {
                EndGame();
                return;
            }
            ResetAnswers();
            state.Question = questions[currQuestion].Text;
            state.IsActive = true;
            state.ShuffleAnswers(questions[currQuestion].CorrectAnswer, questions[currQuestion].WrongAnswer1, questions[currQuestion].WrongAnswer2, questions[currQuestion].WrongAnswer3);
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

        private void ResetAnswers()
        {
            first = null;
            second = null;
            state.First = null;
            state.Second = null;
            state.CorrectAnswer = null;
            for (int i = 0; i < playerCount; i++)
            {
                state.CanAnswer[i] = true;
                state.PlayerAnswers[i] = null;
                answers[i] = null;
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
        private void Next(Object source, ElapsedEventArgs e)
        {
            nextTimer.Stop();
            NextQuestion();
        }

        private void ShowAnswer()
        {
            timer.Stop();
            state.IsActive = false;
            state.PlayerAnswers = answers;
            state.CorrectAnswer = state.Answers.IndexOf(questions[currQuestion].CorrectAnswer);
            state.First = first;
            state.Second = second;
            if (first != null)
                state.Players[(int)first].Points += questions[currQuestion].Points;
            if(second!=null)
                state.Players[(int)second].Points += questions[currQuestion].Points/2;
            nextTimer.Start();
            _match.SendUpdate(GetState());
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

        public GameState GetState()
        {
            state.TimerValue = timerValue;
            return state;
        }

        public void Quit()
        {
            timer.Stop();
            timer.Dispose();
        }

        public void SubmitAnswer(Answer answer, string username)
        {
            if(state.IsActive)
            {
                bool allAnswered = true;
                for(int i=0;i<playerCount;i++)
                {
                    if(username==state.Players[i].User.Username && state.CanAnswer[i]==true)
                    {
                        answers[i] = state.Answers.IndexOf(answer.Text);
                        state.CanAnswer[i] = false;
                        if(answer.Text==questions[currQuestion].CorrectAnswer)
                        {
                            if(first==null)
                                first = i;
                            else if(second==null)
                                second = i;
                        }
                    }
                    if (state.CanAnswer[i])
                        allAnswered = false;
                }
                if (allAnswered)
                    ShowAnswer();
            }
        }
    }
}
