using Microsoft.AspNetCore.Http;
using Server.Logic.DTOs;
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

        public LobbyService(IRoomMaster roomMaster, IBaseRepository<User> baseRepository)
        {
            _roomMaster = roomMaster;
            _baseRepository = baseRepository;
        }
        public Room FindRoom(string id)
        {
            return _roomMaster.FindRoom(id);
        }

        public List<RoomDTO> GetRooms()
        {
            List<Room> rooms= _roomMaster.FreeRooms();
            List<RoomDTO> roomsList = new List<RoomDTO>();
            foreach(Room r in rooms)
            {
                roomsList.Add(new RoomDTO(r));
            }
            return roomsList;
        }

        public void AddToQueue(string conncetionId)
        {
            _roomMaster.AddToQueue(conncetionId);
        }

        void ILobbyService.RemoveFromQueue(string connectionId)
        {
            _roomMaster.RemoveFromQueue(connectionId);
        }

        public string CreateRoom(string username)
        {
            User u = _baseRepository.FindOne(x => x.Username == username);
            UserDTO user = new UserDTO(u);
            return _roomMaster.CreateRoom(user).RoomID;
        }
    }
}
