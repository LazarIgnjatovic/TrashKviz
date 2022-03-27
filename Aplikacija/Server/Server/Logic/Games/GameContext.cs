using Newtonsoft.Json;
using Server.Logic.DTOs;
using Server.Logic.Games.ConcreteGames;
using Server.Logic.Masters.Match;
using Server.Logic.Services;
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
        private readonly IStandardStringService _standardStringService;

        public GameContext(IBaseRepository<Question> baseQuestionRepository,IStandardStringService standardStringService)
        {
            _baseQuestionRepository = baseQuestionRepository;
            _standardStringService = standardStringService;
        }

        public void SetGame(GameType gameType)
        {
            if(game!=null)
                game.Quit();
            switch (gameType)
            {
                case GameType.Association:
                    game = new AssociationGame(_baseQuestionRepository,_standardStringService,_match);
                    break;
                case GameType.ClosestNumber:
                    game = new ClosestNumberGame(_baseQuestionRepository,_match);
                    break;
                case GameType.MultipleChoice:
                    game = new MultipleChoiceGame(_baseQuestionRepository,_match);
                    break;
                case GameType.StepByStep:
                    game = new StepByStepGame(_baseQuestionRepository,_standardStringService,_match);
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

        internal void FlagConnected(string userIdentifier)
        {
            game.FlagConnected(userIdentifier);
        }

        internal void FlagDisconnected(string userIdentifier)
        {
            game.FlagDisconnected(userIdentifier);
        }

        internal void Quit()
        {
            game.Quit();
            game = null;
        }
    }
}
