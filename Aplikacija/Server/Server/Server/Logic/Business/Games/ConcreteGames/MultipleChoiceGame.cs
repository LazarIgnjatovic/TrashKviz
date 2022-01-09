using Server.Logic.Business.DTOs;
using Server.Logic.Business.DTOs.ConcreteGameStates;
using Server.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Business.Games
{
    public class MultipleChoiceGame : IGame
    {
        private MultipleChoice[] questions;
        private MultipleChoiceState state;
        public void SubmitAnswer(Answer answer)
        {
            throw new NotImplementedException();
        }    }
}
