using Server.Logic.DTOs;
using Server.Logic.DTOs.ConcreteGameStates;
using Server.Logic.Masters.Match;
using Server.Persistence.Models;
using Server.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using System.Timers;
using Server.Logic.DTOs.ConcreteAnswers;

namespace Server.Logic.Games.ConcreteGames
{
    public class AssociationGame : IGame
    {
        private readonly IBaseRepository<Question> _baseQuestionRepository;
        private Association association;
        private AssociationState state;
        private Match _match;
        private int timerValue;
        private Timer timer;
        private int turnTimerValue;
        private Timer turnTimer;
        private Timer endTimer;
        private int playerCount;
        private int perTurn = 15;
        private int gameLen = 300;

        public AssociationGame(IBaseRepository<Question> baseQuestionRepository, Match match)
        {
            _baseQuestionRepository = baseQuestionRepository;
            _match = match;
            association = _baseQuestionRepository.Sample<Association>(1)[0];
            StartGame();
        }
        private void StartGame()
        {
            playerCount = _match.Players.Count();
            state = new AssociationState(_match.Players);
            state.OnTurn = 0;
            timerValue = gameLen;
            timer = new Timer(1000);
            timer.AutoReset = true;
            timer.Elapsed += Tick;
            timer.Enabled = true;

            turnTimerValue = perTurn;
            turnTimer = new Timer(1000);
            turnTimer.AutoReset = true;
            turnTimer.Elapsed += TurnTick;
            turnTimer.Enabled = true;
            _match.SendUpdate(GetState());
        }
        private void Tick(Object source, ElapsedEventArgs e)
        {
            timerValue--;
            if (timerValue <= 0)
            {
                timer.Stop();
                ShowSolution();
            }
        }
        private void TurnTick(Object source, ElapsedEventArgs e)
        {
            turnTimerValue--;
            if (turnTimerValue <= 0)
            {
                turnTimer.Stop();
                NextPlayer();
            }
        }

        private void NextPlayer()
        {
            int currPlayer = state.OnTurn;
            do
            {
                currPlayer = (currPlayer + 1) % playerCount;
                state.OnTurn = currPlayer;
            } while (!state.Players[state.OnTurn].IsConnected);
            state.OpenAllowed = false;
            turnTimerValue = perTurn;
            turnTimer.Start();
            _match.SendUpdate(GetState());
        }

        private void ShowSolution()
        {
            for(int i=0;i<association.Columns.Length;i++)
            {
                for(int j=0;j<association.Columns[0].Fields.Length;j++)
                {
                    state.Columns[i][j] = association.Columns[i].Fields[j];
                }
                state.ColumnAnswers[i] = association.Columns[i].Answer;
            }
            state.FinalAnswer = association.Answer;
            state.OpenAllowed = false;
            state.TimerValue = 5;
            state.TurnTimerValue = 0;
            _match.SendUpdate(state);

            endTimer = new Timer(5000);
            endTimer.AutoReset = false;
            endTimer.Elapsed += EndGame;
            endTimer.Enabled = true;
        }
        private void EndGame(Object source, ElapsedEventArgs e)
        {
            turnTimer.Stop();
            turnTimer.Dispose();
            timer.Stop();
            timer.Dispose();
            endTimer.Stop();
            endTimer.Dispose();
            _match.Players = state.Players;
            _match.StartNextGame();
        }

        public void SubmitAnswer(Answer _answer,string username)
        {
            AssociationAnswer answer = (AssociationAnswer)_answer;
            if(username==state.Players[state.OnTurn].User.Username)
            {
                if(answer.IsFinalAnswer)
                {
                    if (answer.Text == association.Answer)
                    {
                        AddPoints(association.Points);
                        ShowSolution();
                    }
                    else
                        NextPlayer();
                }
                else if(answer.IsColumnAnswer)
                {
                    if (association.Columns[answer.Column].Answer == answer.Text)
                    {
                        AddPoints(association.Points / association.Columns.Length);
                        for (int i = 0; i < association.Columns[answer.Column].Fields.Length;i++)
                        {
                            state.Columns[answer.Column][i] = association.Columns[answer.Column].Fields[i];
                        }
                        state.ColumnAnswers[answer.Column] = association.Columns[answer.Column].Answer;
                        _match.SendUpdate(state);
                    }
                    else
                        NextPlayer();
                }
                else if (state.OpenAllowed)
                {
                    state.OpenAllowed = false;
                    state.Columns[answer.Column][answer.Field] = association.Columns[answer.Column].Fields[answer.Field];
                }
            }
        }
        private void AddPoints(int points)
        {
            state.Players[state.OnTurn].Points += points;
        }

        public GameState GetState()
        {
            state.TimerValue = timerValue;
            state.TurnTimerValue = turnTimerValue;
            return state;
        }
    }
}
