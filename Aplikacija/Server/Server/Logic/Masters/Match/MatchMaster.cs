using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Server.Logic.DTOs;

namespace Server.Logic.Masters.Match
{
    public class MatchMaster : IMatchMaster
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private List<Match> activeMatches;

        public MatchMaster(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            activeMatches = new List<Match>();
        }

        public bool CheckReconnect(string userIdentifier)
        {
           return activeMatches.Exists(m => m.Players.Exists(p => p.User.Username == userIdentifier));
        }

        public bool MatchCodeExists(string roomId)
        {
            return activeMatches.Exists(m => m.MatchID == roomId);
        }

        public void StartGame(Room.Room r)
        {
            Match newMatch = new Match(_serviceScopeFactory);
            newMatch.StartMatch(r);
            activeMatches.Add(newMatch);
        }
        public void SubmitAnswer(Answer answer, string matchId, string username)
        {
            foreach(Match match in activeMatches)
            {
                if(matchId==match.MatchID)
                {
                    match.SubmitAnswer(answer, username);
                }
            }
        }

        public string UserConnected(string userIdentifier)
        {
            Match match = activeMatches.Where(x => x.Players.Exists(p => p.User.Username == userIdentifier)).FirstOrDefault();
            if(match!=null)
            {
                match.FlagConnected(userIdentifier);
                return match.MatchID;
            }
            return "";
        }

        public void UserDisconnected(string userIdentifier)
        {
            Match match = activeMatches.Where(x => x.Players.Exists(p => p.User.Username == userIdentifier)).FirstOrDefault();
            if(match!=null)
            {
                match.FlagDisconnected(userIdentifier);
            }
        }
    }
}
