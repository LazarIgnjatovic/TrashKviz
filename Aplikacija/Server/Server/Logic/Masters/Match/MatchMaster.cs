using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Server.Logic.DTOs;

namespace Server.Logic.Masters.Match
{
    public class MatchMaster : IMatchMaster
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private Dictionary<string,Match> activeMatches;

        public MatchMaster(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            activeMatches = new Dictionary<string, Match>();
        }

        public bool CheckReconnect(string userIdentifier)
        {
           return activeMatches.Values.Any(m => m.Players.Exists(p => p.User.Username == userIdentifier));
        }

        public void DestroyMe(Match match)
        {
            activeMatches.Remove(match.MatchID);
        }

        public bool MatchCodeExists(string roomId)
        {
            return activeMatches.Values.Any(m => m.MatchID == roomId);
        }

        public void StartGame(Room.Room r)
        {
            Match newMatch = new Match(_serviceScopeFactory,this);
            newMatch.StartMatch(r);
            activeMatches.Add(newMatch.MatchID,newMatch);
        }
        public void SubmitAnswer(Answer answer, string matchId, string username)
        {
            if (activeMatches.ContainsKey(matchId))
            {
                activeMatches[matchId].SubmitAnswer(answer, username);
            }
        }

        public string UserConnected(string userIdentifier)
        {
            Match match = activeMatches.Where(x => x.Value.Players.Exists(p => p.User.Username == userIdentifier)).FirstOrDefault().Value;
            if(match!=null)
            {
                match.FlagConnected(userIdentifier);
                return match.MatchID;
            }
            return "";
        }

        public void UserDisconnected(string userIdentifier)
        {
            Match match = activeMatches.Where(x => x.Value.Players.Exists(p => p.User.Username == userIdentifier)).FirstOrDefault().Value;
            if(match!=null)
            {
                match.FlagDisconnected(userIdentifier);
            }
        }
    }
}
