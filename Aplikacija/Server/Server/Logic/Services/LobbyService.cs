using Server.Logic.Masters.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Services
{
    public class LobbyService : ILobbyService
    {
        private IRoomMaster _roomMaster;

        public LobbyService(IRoomMaster roomMaster)
        {
            _roomMaster = roomMaster;
        }
        public Room FindRoom(string id)
        {
            return _roomMaster.FindRoom(id);
        }

        public Room[] GetRooms()
        {
            return _roomMaster.FreeRooms();
        }


    }
}
