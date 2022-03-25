using Server.Logic.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.DTOs.ConcreteGameStates
{
    public class ClosestNumberState:GameState
    {
        private List<Player> players;
        private List<bool> canAnswer;
        private List<bool> isWinner;
        private string question;
        private float answer;
        private bool active;
        private int timerValue;
        public List<Player> Players { get => players; set => players = value; }
        public string Question { get => question; set => question = value; }
        public float Answer { get => answer; set => answer = value; }
        public bool Active { get => active; set => active = value; }
        public int TimerValue { get => timerValue; set => timerValue = value; }
        public List<bool> CanAnswer { get => canAnswer; set => canAnswer = value; }
        public List<bool> IsWinner { get => isWinner; set => isWinner = value; }

        public ClosestNumberState(List<Player> players)
        {
            Type = GameType.ClosestNumber;
            this.players = players.OrderBy(x => Guid.NewGuid()).ToList();
            canAnswer = new List<bool>(players.Count);
            isWinner = new List<bool>(players.Count);
        }
    }
}
