using Server.Logic.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.DTOs.ConcreteGameStates
{
    public class StepByStepState : GameState
    {
        public string[] Steps { get; set; }
        public string FinalAnswer { get; set; }
        public List<Player> Players { get; set; }
        public bool[] CanAnswer { get; set; }
        public string[] Answers { get; set; }
        public bool IsActive { get; set; }
        public int TimerValue { get; set; }
        public int Winner { get; set; }
        
        public StepByStepState(List<Player> players, int stepCount)
        {
            Type = GameType.StepByStep;
            Players = players;
            Steps = new string[stepCount];
            CanAnswer = new bool[players.Count];
            Answers = new string[players.Count];
        }
    }
}
