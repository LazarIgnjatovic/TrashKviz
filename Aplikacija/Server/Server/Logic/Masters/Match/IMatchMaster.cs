using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Logic.DTOs;
using Server.Logic.Masters.Room;

namespace Server.Logic.Masters.Match
{
    public interface IMatchMaster
    {
        //TODO
        void StartGame(Room.Room room);
        bool MatchCodeExists(string roomId);
        void SubmitAnswer(Answer answer, string matchId, string username);
        void UserDisconnected(string userIdentifier);
        string UserConnected(string userIdentifier);
        bool CheckReconnect(string userIdentifier);
    }
}
