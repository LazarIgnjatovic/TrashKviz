using Server.Logic.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.DTOs
{
    public abstract class GameState
    {
        public int TimerValue { get; set; }
        public List<Player> Players { get; set; }
        public GameType Type { get; set; }
    }
}
