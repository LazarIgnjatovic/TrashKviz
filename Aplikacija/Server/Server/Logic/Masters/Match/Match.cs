using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Server.Communication.Hubs;
using Server.Logic.DTOs;
using Server.Logic.DTOs.ConcreteGameStates;
using Server.Logic.Games;
using Server.Logic.Services;
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
        private int maxConnected;
        private int currConnected;
        private readonly IHubContext<MatchHub> _matchHubContext;
        private readonly IStandardStringService _standardStringService;
        private readonly IBaseRepository<Question> _baseQuestionRepository;
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMatchMaster _matchMaster;
        private IServiceScope scope;

        public string MatchID { get => matchID; set => matchID = value; }
        public List<Player> Players { get => players; set => players = value; }
        public List<GameType> Games { get => games; set => games = value; }
        public int CurrentGame { get => currentGame; set => currentGame = value; }
        public GameContext Game { get => gameContext; set => gameContext = value; }

        public Match(IServiceScopeFactory serviceScopeFactory,IMatchMaster matchMaster)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _matchMaster = matchMaster;
            scope = _serviceScopeFactory.CreateScope();
            _baseQuestionRepository = scope.ServiceProvider.GetService<IBaseRepository<Question>>();
            _baseUserRepository = scope.ServiceProvider.GetService<IBaseRepository<User>>();
            _matchHubContext = scope.ServiceProvider.GetService<IHubContext<MatchHub>>();
            _standardStringService = scope.ServiceProvider.GetService<IStandardStringService>();
            players = new List<Player>();
            games = new List<GameType>();
            maxConnected = 0;
        }

        internal void FlagConnected(string userIdentifier)
        {
            Player connected = players.Where(x => x.User.Username == userIdentifier).FirstOrDefault();
            connected.IsConnected = true;
            gameContext.FlagConnected(userIdentifier);
            SendUpdate(gameContext.game.GetState());
            _matchHubContext.Clients.User(userIdentifier).SendAsync("MatchUpdate", gameContext.game.GetState());
            currConnected++;
            if (currConnected>maxConnected)
            {
                maxConnected = currConnected;
            }
                
        }

        internal void FlagDisconnected(string userIdentifier)
        {
            Player afk = players.Where(x => x.User.Username == userIdentifier).FirstOrDefault();
            afk.IsConnected = false;
            gameContext.FlagDisconnected(userIdentifier);
            SendUpdate(gameContext.game.GetState());
            currConnected--;
            if (maxConnected==players.Count&&currConnected==0)
            {
                gameContext.Quit();
                _matchMaster.DestroyMe(this);
            }       
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
            gameContext = new GameContext(_baseQuestionRepository,_standardStringService);
            gameContext.SetMatch(this);
        }
        public void GameEnded()
        {
            if(currentGame<games.Count-1)
                gameContext.SetGame(GameType.Info);
            else
                ConcludeMatch();
        }
        public void StartNextGame()
        {
            currentGame++;
            if(currentGame<games.Count)
            {
                gameContext.SetGame(games[currentGame]);
                SendUpdate(gameContext.game.GetState());
                return;
            }
            ConcludeMatch();
        }

        private void ConcludeMatch()
        {
            players = players.OrderBy(x => x.Points).ToList();
            _matchHubContext.Clients.Group(matchID).SendAsync("MatchEnd", players);
            for (int i=0;i<players.Count;i++)
            {
                User u = _baseUserRepository.FindOne(x => x.Username == players[i].User.Username);
                float gamesWon = u.Stats.Winrate * u.Stats.GamesPlayed;
                if (i==0)
                    gamesWon += 1;
                u.Stats.GamesPlayed += 1;
                u.Stats.Winrate = gamesWon / u.Stats.GamesPlayed;
                _baseUserRepository.ReplaceOne(u);
            }
            gameContext.Quit();
            _matchMaster.DestroyMe(this);
        }

        public void SubmitAnswer(Answer answer,string username)
        {
            gameContext.SubmitAnswer(answer, username);
        }

        internal void SendUpdate(GameState state)
        {
            _matchHubContext.Clients.Group(matchID).SendAsync("MatchUpdate", state);
        }
        internal void Tick()
        {
            _matchHubContext.Clients.Group(matchID).SendAsync("Tick");
        }
        internal void OnTurn(List<string> users)
        {
            foreach (Player p in players)
            {
                if (users.Exists(x=>x==p.User.Username))
                    _matchHubContext.Clients.User(p.User.Username).SendAsync("OnTurn");
                else
                    _matchHubContext.Clients.User(p.User.Username).SendAsync("NotOnTurn");
            }
        }
    }
}
