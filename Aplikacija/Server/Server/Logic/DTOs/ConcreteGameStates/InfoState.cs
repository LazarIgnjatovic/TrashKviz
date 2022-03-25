using Server.Logic.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.DTOs.ConcreteGameStates
{
    public class InfoState:GameState
    {
        private string infoText;
        private int timerValue;
        public string InfoText { get => infoText; set => infoText = value; }
        public int TimerValue { get => timerValue; set => timerValue = value; }
        public InfoState()
        {
            Type = GameType.Info;
        }
    }
}
