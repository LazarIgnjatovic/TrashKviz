using Server.Logic.DTOs;
using Server.Logic.Masters.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchMaster _matchMaster;

        public MatchService(IMatchMaster matchMaster)
        {
            _matchMaster = matchMaster;
        }
        public void SubmitAnswer(Answer answer, string matchId, string username)
        {
            _matchMaster.SubmitAnswer(answer, matchId, username);
        }

        public string UserConnected(string userIdentifier)
        {
            return _matchMaster.UserConnected(userIdentifier);
        }

        public void UserDisconnected(string userIdentifier)
        {
            _matchMaster.UserDisconnected(userIdentifier);
        }
    }
}
