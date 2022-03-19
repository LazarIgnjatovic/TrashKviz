using Server.Logic.Masters.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Services
{
    public interface ILobbyService
    {
        public Room[] GetRooms();
        public Room FindRoom(string id);

    }
}
