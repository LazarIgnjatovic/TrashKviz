using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Server.Communication.Hubs;
using Server.Logic.DTOs;
using Server.Logic.DTOs.ConcreteGameStates;
using Server.Logic.Games;
using Server.Persistence.Models;
using Server.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Masters.Match
{
    public class Match
    {
        private string matchID;
        private List<Player> players;
        private List<GameType> games;
        private int currentGame;
        private GameContext gameContext;
        private readonly IHubContext<MatchHub> _matchHubContext;
        private readonly IBaseRepository<Question> _baseQuestionRepository;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private IServiceScope scope;

        public string MatchID { get => matchID; set => matchID = value; }
        public List<Player> Players { get => players; set => players = value; }
        public List<GameType> Games { get => games; set => games = value; }
        public int CurrentGame { get => currentGame; set => currentGame = value; }
        public GameContext Game { get => gameContext; set => gameContext = value; }

        public Match(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            scope = _serviceScopeFactory.CreateScope();
            _baseQuestionRepository = scope.ServiceProvider.GetService<IBaseRepository<Question>>();
            _matchHubContext = scope.ServiceProvider.GetService<IHubContext<MatchHub>>();
            players = new List<Player>();
            games = new List<GameType>();
        }

        internal void FlagConnected(string userIdentifier)
        {
            Player connected = players.Where(x => x.User.Username == userIdentifier).FirstOrDefault();
            connected.IsConnected = true;
            SendUpdate(gameContext.game.GetState());
            _matchHubContext.Clients.User(userIdentifier).SendAsync("MatchUpdate", gameContext.game.GetState());
        }

        internal void FlagDisconnected(string userIdentifier)
        {
            Player afk = players.Where(x => x.User.Username == userIdentifier).FirstOrDefault();
            afk.IsConnected = false;
            SendUpdate(gameContext.game.GetState());
        }

        public void StartMatch(Room.Room room)
        {
            matchID = room.roomId;
            foreach (RoomUserDTO user in room.users)
                players.Add(new Player(user));
            games.Add(room.game1);
            games.Add(room.game2);
            games.Add(room.game3);
            currentGame = -1;
            gameContext = new GameContext(_baseQuestionRepository);
            gameContext.SetMatch(this);
            gameContext.SetGame(GameType.Info);
        }
        public void GameEnded()
        {
            gameContext.SetGame(GameType.Info);
        }
        public void StartNextGame()
        {
            currentGame++;
            if(currentGame<3)
            {
                gameContext.SetGame(games[currentGame]);
                SendUpdate(gameContext.game.GetState());
            }
            //kraj igre
        }
        public void SubmitAnswer(Answer answer,string username)
        {
            gameContext.SubmitAnswer(answer, username);
        }

        internal void SendUpdate(GameState state)
        {
            _matchHubContext.Clients.Group(matchID).SendAsync("MatchUpdate", state);
        }
    }
}
