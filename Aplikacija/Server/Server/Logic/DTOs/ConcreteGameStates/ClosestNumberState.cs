using Server.Logic.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.DTOs.ConcreteGameStates
{
    public class ClosestNumberState:GameState
    {
        private bool[] canAnswer;
        private bool[] isWinner;
        private string question;
        private float? answer;
        private float?[] answers;
        private bool isActive;
        public string Question { get => question; set => question = value; }
        public float? Answer { get => answer; set => answer = value; }
        public bool[] CanAnswer { get => canAnswer; set => canAnswer = value; }
        public bool[] IsWinner { get => isWinner; set => isWinner = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public float?[] Answers { get => answers; set => answers = value; }

        public ClosestNumberState(List<Player> players)
        {
            Type = GameType.ClosestNumber;
            Players = players.OrderBy(x => Guid.NewGuid()).ToList();
            canAnswer = new bool[players.Count];
            isWinner = new bool[players.Count];
            answers = new float?[players.Count];
        }
    }
}
