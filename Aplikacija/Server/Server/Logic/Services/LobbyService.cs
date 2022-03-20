using Server.Logic.Masters.Room;
using Server.Persistence.Models;
using Server.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Services
{
    public class LobbyService : ILobbyService
    {
        private readonly IRoomMaster _roomMaster;
        private readonly IBaseRepository<User> _baseRepository;

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

        public void AddToQueue(string conncetionId)
        {
            
        }

    }
}
