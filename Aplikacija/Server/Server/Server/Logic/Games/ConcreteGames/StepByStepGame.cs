using Server.Logic.DTOs;
using Server.Logic.DTOs.ConcreteGameStates;
using Server.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Games
{
    public class StepByStepGame : IGame
    {
        private StepByStep stepByStep;
        private StepByStepState state;
        public void SubmitAnswer(Answer answer)
        {
            throw new NotImplementedException();
        }
    }
}
