using Microsoft.Extensions.DependencyInjection;
using Server.Logic.DTOs;
using Server.Logic.Games.ConcreteGames;
using Server.Logic.Masters.Match;
using System;

namespace Server.Logic.Games
{
    public class GameContext
    {
        public IGame game;
        public Match _match;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public GameContext(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void SetGame(GameType gameType)
        {
            if(game!=null)
                game.Quit();
            Type type = Type.GetType("Server.Logic.Games.ConcreteGames."+gameType.ToString());
            game = (IGame)Activator.CreateInstance(type,_match,_serviceScopeFactory);
            
        }
        public void SubmitAnswer(Answer answer,string username)
        {
            game.SubmitAnswer(answer,username);
        }

        internal void SetMatch(Match match)
        {
            _match = match;
            SetGame(GameType.GameInfo);
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
