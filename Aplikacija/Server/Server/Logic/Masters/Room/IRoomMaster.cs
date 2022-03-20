using Server.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Masters.Room
{
    public interface IRoomMaster
    {
        Room FindRoom(string id);
        Room[] FreeRooms();
        void AddToQueue(string connectionId);
        void RemoveFromQueue(string connectionId);
        Room CreateRoom();
    }
}
