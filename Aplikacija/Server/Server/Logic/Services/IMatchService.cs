using Server.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Services
{
    public interface IMatchService
    {
        void SubmitAnswer(Answer answer, string matchId, string username);
        void UserDisconnected(string userIdentifier);
        string UserConnected(string userIdentifier);
    }
}
