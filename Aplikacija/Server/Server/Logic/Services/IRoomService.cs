using Server.Logic.Masters.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Services
{
    public interface IRoomService
    {
        public Room JoinRoom(string username,string roomId);
        public Room LeaveRoom(string username,string roomId);
        public Room ModifyRoom(string username,Room room);
        public Room MarkReady(string username, string roomId);
        public Room UnmarkReady(string username, string roomId);
        public Room UserDisconnected(string username);
        public bool StartGame(string username,string roomId); 
    }
}
