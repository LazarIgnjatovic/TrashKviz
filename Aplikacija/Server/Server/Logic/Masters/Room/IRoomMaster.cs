using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Masters.Room
{
    public interface IRoomMaster
    {
        //TODO
        Room FindRoom(string id);
        Room[] FreeRooms();
    }
}
