using Server.Logic.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.DTOs.ConcreteGameStates
{
    public class ClosestNumberState:GameState
    {
        private List<bool> canAnswer;
        private List<bool> isWinner;
        private string question;
        private float answer;
        private bool isActive;
        public string Question { get => question; set => question = value; }
        public float Answer { get => answer; set => answer = value; }
        public List<bool> CanAnswer { get => canAnswer; set => canAnswer = value; }
        public List<bool> IsWinner { get => isWinner; set => isWinner = value; }
        public bool IsActive { get => isActive; set => isActive = value; }

        public ClosestNumberState(List<Player> players)
        {
            Type = GameType.ClosestNumber;
            Players = players.OrderBy(x => Guid.NewGuid()).ToList();
            canAnswer = new List<bool>(players.Count);
            isWinner = new List<bool>(players.Count);
        }
    }
}
