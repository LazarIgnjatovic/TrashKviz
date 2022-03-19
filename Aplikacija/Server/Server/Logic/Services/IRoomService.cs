using Server.Logic.Masters.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Services
{
    public interface IRoomService
    {
        public Room JoinRoom(string roomId);
        public void LeaveRoom(string roomId);
        public void ModifyRoom(Room room);
        public void MarkReady(string username, string roomId);
        public bool StartGame(string roomId); 
    }
}
