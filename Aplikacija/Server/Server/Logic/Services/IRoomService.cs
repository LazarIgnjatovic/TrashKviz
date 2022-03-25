using Server.Logic.Masters.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Services
{
    public interface IRoomService
    {
        Room LeaveRoom(string username,string roomId);
        Room ModifyRoom(string username,Room room);
        Room MarkReady(string username, string roomId);
        Room UserDisconnected(string username);
        bool StartGame(string username,string roomId);
        Room ConnectedUser(string username);
        void Kick(string roomID, string username, string userIdentifier);
        void CheckReady(Room r);
    }
}
