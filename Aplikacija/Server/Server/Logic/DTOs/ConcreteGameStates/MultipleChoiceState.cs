using Server.Logic.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.DTOs.ConcreteGameStates
{
    public class MultipleChoiceState : GameState
    {
        public string Question { get; internal set; }
        public bool IsActive { get; internal set; }
        public List<string> Answers { get; set; }
        public int? CorrectAnswer { get; set; }
        public int? First { get; set; }
        public int? Second { get; set; }
        public int?[] PlayerAnswers { get; set; }
        public bool[] CanAnswer { get; internal set; }

        public MultipleChoiceState(List<Player> players)
        {
            Type = GameType.MultipleChoice;
            Players = players.OrderBy(x => Guid.NewGuid()).ToList();
            Answers = new List<string>();
            PlayerAnswers = new int?[Players.Count];
            CanAnswer = new bool[Players.Count];
        }
        internal void ShuffleAnswers(string correctAnswer, string wrongAnswer1, string wrongAnswer2, string wrongAnswer3)
        {
            Answers.Clear();
            Answers.Add(correctAnswer);
            Answers.Add(wrongAnswer1);
            Answers.Add(wrongAnswer2);
            Answers.Add(wrongAnswer3);
            Answers = Answers.OrderBy(x => Guid.NewGuid()).ToList();
        }
    }
}
