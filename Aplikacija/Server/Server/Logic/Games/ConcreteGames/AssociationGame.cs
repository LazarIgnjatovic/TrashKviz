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
using Server.Logic.Services;

namespace Server.Logic.Games.ConcreteGames
{
    public class AssociationGame : IGame
    {
        private readonly IBaseRepository<Question> _baseQuestionRepository;
        private readonly IStandardStringService _standardStringService;
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
        private Timer wrongTimer;

        public AssociationGame(IBaseRepository<Question> baseQuestionRepository,IStandardStringService standardStringService, Match match)
        {
            _baseQuestionRepository = baseQuestionRepository;
            _standardStringService = standardStringService;
            _match = match;
            association = _baseQuestionRepository.Sample<Association>(1)[0];
            StartGame();
        }
        private void StartGame()
        {
            playerCount = _match.Players.Count();
            state = new AssociationState(_match.Players);
            state.OnTurn = 0;
            state.OpenAllowed = true;
            state.ShowTimer = true;
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

            endTimer = new Timer(5000);
            endTimer.AutoReset = false;
            endTimer.Elapsed += EndGame;

            wrongTimer = new Timer(3000);
            wrongTimer.AutoReset = false;
            wrongTimer.Elapsed += Next;
            SendOnTurn();

            _match.SendUpdate(GetState());
        }

        private void Next(object sender, ElapsedEventArgs e)
        {
            wrongTimer.Stop();
            NextPlayer();
        }

        private void Tick(Object source, ElapsedEventArgs e)
        {
            _match.Tick();
            timerValue--;
            if (timerValue == 0)
            {
                timerValue--;
                timer.Stop();
                ShowSolution();
            }
        }
        private void TurnTick(Object source, ElapsedEventArgs e)
        {
            turnTimer.Stop();
            turnTimerValue--;
            if (turnTimerValue == 0)
            {
                NextPlayer();
            }
            else
                turnTimer.Start();
        }

        private void NextPlayer()
        {
            turnTimer.Stop();
            state.ShowTimer = true;
            int currPlayer = state.OnTurn;
            do
            {
                currPlayer = (currPlayer + 1) % playerCount;
                state.OnTurn = currPlayer;
            } while (!state.Players[state.OnTurn].IsConnected);
            state.OpenAllowed = true;
            ResetAnswers();
            SendOnTurn();
            turnTimerValue = perTurn;
            _match.SendUpdate(GetState());
            turnTimer.Start();
        }

        private void ResetAnswers()
        {
            state.FinalAnswer = null;
            for(int i=0;i<state.ColumnAnswers.Length; i++)
            {
                if(state.ColumnAnswers[i]!=null && state.ColumnAnswers[i][0]=='#')
                    state.ColumnAnswers[i] = null;
            }
        }

        private void ShowSolution()
        {
            timer.Stop();
            turnTimer.Stop();
            wrongTimer.Stop();
            state.ShowTimer = false;
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

            
            endTimer.Start();
        }
        private void EndGame(Object source, ElapsedEventArgs e)
        {
            Quit();
            _match.Players = state.Players;
            _match.GameEnded();
        }

        public void SubmitAnswer(Answer _answer,string username)
        {
            AssociationAnswer answer = (AssociationAnswer)_answer;
            if (username==state.Players[state.OnTurn].User.Username)
            {
                if(answer.IsFinalAnswer)
                {
                    if (_standardStringService.Standardize(answer.Text) == _standardStringService.Standardize(association.Answer))
                    {
                        AddPoints(association.Points);
                        ShowSolution();
                        return;
                    }
                    else
                    {
                        state.FinalAnswer = "#" + answer.Text;
                        ShowWrongAnswer();
                        return;
                    }
                }
                else if(answer.IsColumnAnswer)
                {
                    if (_standardStringService.Standardize(association.Columns[answer.Column].Answer) == _standardStringService.Standardize(answer.Text))
                    {
                        AddPoints(association.Points / association.Columns.Length);
                        for (int i = 0; i < association.Columns[answer.Column].Fields.Length;i++)
                        {
                            state.Columns[answer.Column][i] = association.Columns[answer.Column].Fields[i];
                        }
                        state.ColumnAnswers[answer.Column] = association.Columns[answer.Column].Answer;
                        _match.SendUpdate(state);
                        return;
                    }
                    else
                    {
                        state.ColumnAnswers[answer.Column] = "#" + answer.Text;
                        ShowWrongAnswer();
                        return;
                    }
                }
                else if (state.OpenAllowed)
                {
                    state.OpenAllowed = false;
                    state.Columns[answer.Column][answer.Field] = association.Columns[answer.Column].Fields[answer.Field];
                    _match.SendUpdate(GetState());
                }
            }
        }

        private void ShowWrongAnswer()
        {
            turnTimer.Stop();
            state.OpenAllowed = false;
            state.ShowTimer = false;
            List<string> onTurn = new List<string>();
            _match.OnTurn(onTurn);
            _match.SendUpdate(GetState());
            wrongTimer.Start();
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
        public void FlagConnected(string username)
        {
            Player p = state.Players.Where(x => x.User.Username == username).FirstOrDefault();
            if (p != null)
            {
                p.IsConnected = true;
                SendOnTurn();
            }
        }

        public void FlagDisconnected(string username)
        {
            Player p = state.Players.Where(x => x.User.Username == username).FirstOrDefault();
            if (p != null)
                p.IsConnected = false;
        }
        private void SendOnTurn()
        {
            List<string> onTurn = new List<string>();
            onTurn.Add(state.Players[state.OnTurn].User.Username);
            _match.OnTurn(onTurn);
        }

        public void Quit()
        {
            turnTimer.Stop();
            turnTimer.Dispose();
            timer.Stop();
            timer.Dispose();
            endTimer.Stop();
            endTimer.Dispose();
            wrongTimer.Stop();
            wrongTimer.Dispose();
        }
    }
}
