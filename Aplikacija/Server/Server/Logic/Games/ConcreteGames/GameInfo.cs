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

        public GameInfo(Match match)
        {
            state = new InfoState();
            this._match = match;
            infoText = "Sledeća igra počinje za:";
            timerValue = 5;
            timer = new Timer(1000);
            timer.AutoReset = true;
            timer.Elapsed += Tick;
            timer.Enabled = true;

        }

        private void Tick(Object source,ElapsedEventArgs e)
        {
            timerValue--;
            if(timerValue<=0)
            {
                timer.Stop();
                timer.Dispose();
                _match.StartNextGame();
            }
        }
        public void SubmitAnswer(Answer answer, string username)
        {
            throw new NotImplementedException();
        }

        public GameState GetState()
        {
            state.InfoText = infoText;
            state.TimerValue = timerValue;
            return state;
        }
    }
}
