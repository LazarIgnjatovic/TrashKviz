using Server.Logic.Masters.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Services
{
    public interface ILobbyService
    {
        Room[] GetRooms();
        Room FindRoom(string id);
        void AddToQueue(string connectionId);
        void RemoveFromQueue(string conncectionId);

    }
}
