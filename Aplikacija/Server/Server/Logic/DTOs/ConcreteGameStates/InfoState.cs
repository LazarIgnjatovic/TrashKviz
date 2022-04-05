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
        public string InfoText { get => infoText; set => infoText = value; }
        public InfoState(List<Player>players)
        {
            Players = players;
            Type = GameType.GameInfo;
        }
    }
}
