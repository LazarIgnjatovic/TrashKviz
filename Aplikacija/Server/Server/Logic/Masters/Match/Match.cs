using Server.Logic.DTOs;
using Server.Logic.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Masters.Match
{
    public class Match
    {
        private string matchID;
        private Player[] players;
        private GameType game1;
        private GameType game2;
        private GameType game3;
        private GameContext game;

        public string MatchID { get => matchID; set => matchID = value; }
    }
}
