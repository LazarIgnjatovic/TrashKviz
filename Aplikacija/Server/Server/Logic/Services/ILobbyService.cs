using Server.Logic.DTOs;
using Server.Logic.Masters.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Services
{
    public interface ILobbyService
    {
        List<RoomDTO> GetRooms();
        Room FindRoom(string id, string username);
        void AddToQueue(string connectionId);
        void RemoveFromQueue(string connectionId);
        string CreateRoom(string username);
        bool CheckReconnect(string userIdentifier);
    }
}
