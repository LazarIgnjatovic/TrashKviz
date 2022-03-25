using Server.Logic.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.DTOs.ConcreteGameStates
{
    public class AssociationState:GameState
    {
        private List<Player> players;
        private string[][] columns;
        private string[] columnAnswers;
        private string finalAnswer;
        private int onTurn;
        private bool openAllowed;
        private int timerValue;
        private int turnTimerValue;

        public List<Player> Players { get => players; set => players = value; }
        public int OnTurn { get => onTurn; set => onTurn = value; }
        public int TimerValue { get => timerValue; set => timerValue = value; }
        public int TurnTimerValue { get => turnTimerValue; set => turnTimerValue = value; }
        public bool OpenAllowed { get => openAllowed; set => openAllowed = value; }
        public string[][] Columns { get => columns; set => columns = value; }
        public string[] ColumnAnswers { get => columnAnswers; set => columnAnswers = value; }
        public string FinalAnswer { get => finalAnswer; set => finalAnswer = value; }

        public AssociationState(List<Player> players)
        {
            Type = GameType.Association;
            this.players = players.OrderBy(x => Guid.NewGuid()).ToList();
            columns = new string[4][];
            columnAnswers = new string[4];
            for (int i = 0; i < 4; i++)
            {
                columns[i] = new string[4];
            }
        }
    }
}
