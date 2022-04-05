using Microsoft.Extensions.DependencyInjection;
using Server.Logic.DTOs;
using Server.Logic.DTOs.ConcreteGameStates;
using Server.Logic.Masters.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Server.Logic.Games.ConcreteGames
{
    public class GameInfo : IGame
    {
        private string infoText;
        private int timerValue;
        private Match _match;
        private Timer timer;
        private InfoState state;
        public string InfoText { get => infoText; set => infoText = value; }
        public int TimerValue { get => timerValue; set => timerValue = value; }

        public GameInfo(Match match, IServiceScopeFactory serviceScopeFactory)
        {
            state = new InfoState(match.Players);
            this._match = match;
            infoText = "Sledeća igra počinje za:";
            timerValue = 3;
            timer = new Timer(1000);
            timer.AutoReset = true;
            timer.Elapsed += Tick;
            timer.Enabled = true;
            _match.SendUpdate(GetState());
        }

        private void Tick(Object source,ElapsedEventArgs e)
        {
            _match.Tick();
            timerValue--;
            if(timerValue<=0)
            {
                Quit();
                _match.StartNextGame();
            }
        }
        public void SubmitAnswer(Answer answer, string username)
        {
            return;
        }

        public GameState GetState()
        {
            state.InfoText = infoText;
            state.TimerValue = timerValue;
            return state;
        }

        public void FlagConnected(string username)
        {
            return;
        }

        public void FlagDisconnected(string username)
        {
            return;
        }

        public void Quit()
        {
            timer.Stop();
            timer.Dispose();
        }
    }
}
