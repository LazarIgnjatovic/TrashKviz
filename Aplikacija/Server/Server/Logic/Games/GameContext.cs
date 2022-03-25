using Newtonsoft.Json;
using Server.Logic.DTOs;
using Server.Logic.Games.ConcreteGames;
using Server.Logic.Masters.Match;
using Server.Persistence.Models;
using Server.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Games
{
    public class GameContext
    {
        public IGame game;
        [JsonIgnore]
        public Match _match;
        private readonly IBaseRepository<Question> _baseQuestionRepository;

        public GameContext(IBaseRepository<Question> baseQuestionRepository)
        {
            _baseQuestionRepository = baseQuestionRepository;
        }

        public void SetGame(GameType gameType)
        {
            switch (gameType)
            {
                case GameType.Association:
                    game = new AssociationGame(_baseQuestionRepository,_match);
                    break;
                case GameType.ClosestNumber:
                    game = new ClosestNumberGame(_baseQuestionRepository,_match);
                    break;
                case GameType.MultipleChoice:
                    game = new MultipleChoiceGame(_baseQuestionRepository,_match);
                    break;
                case GameType.StepByStep:
                    game = new StepByStepGame(_baseQuestionRepository,_match);
                    break;
                case GameType.Info:
                    game = new GameInfo(_match);
                    break;
            }
            
        }
        public void SubmitAnswer(Answer answer,string username)
        {
            game.SubmitAnswer(answer,username);
        }

        internal void SetMatch(Match match)
        {
            _match = match;
            SetGame(GameType.Info);
        }
    }
}
