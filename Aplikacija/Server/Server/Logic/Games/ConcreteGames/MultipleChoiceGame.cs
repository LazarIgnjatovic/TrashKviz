using Server.Logic.DTOs;
using Server.Logic.DTOs.ConcreteGameStates;
using Server.Logic.Masters.Match;
using Server.Persistence.Models;
using Server.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Games.ConcreteGames
{
    public class MultipleChoiceGame : IGame
    {
        private MultipleChoice[] questions;
        private MultipleChoiceState state;
        private Match match;
        private IBaseRepository<Question> baseQuestionRepository;

        public MultipleChoiceGame(Match match)
        {
            this.match = match;
        }

        public MultipleChoiceGame(IBaseRepository<Question> baseQuestionRepository, Match _match)
        {
            this.baseQuestionRepository = baseQuestionRepository;
        }

        public GameState GetState()
        {
            throw new NotImplementedException();
        }

        public void SubmitAnswer(Answer answer)
        {
            throw new NotImplementedException();
        }

        public void SubmitAnswer(Answer answer, string username)
        {
            throw new NotImplementedException();
        }
    }
}
