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
    public class RoomService : IRoomService
    {
        private readonly IRoomMaster _roomMaster;
        private readonly IBaseRepository<User> _baseUserRepository;

        public RoomService(IRoomMaster roomMaster, IBaseRepository<User> baseUserRepository)
        {
            _roomMaster = roomMaster;
            _baseUserRepository = baseUserRepository;
        }
        public Room JoinRoom(string username,string roomId)
        {
            User u = _baseUserRepository.FindOne(x => x.Username == username);
            UserDTO user = new UserDTO(u);
            return _roomMaster.JoinRoom(user, roomId);
        }

        public Room LeaveRoom(string username, string roomId)
        {
            return _roomMaster.LeaveRoom(username, roomId);
        }

        public Room MarkReady(string username, string roomId)
        {
            return _roomMaster.MarkReady(username, roomId);
        }

        public Room ModifyRoom(string username, Room room)
        {
            return _roomMaster.ModifyRoom(username, room);
        }

        public bool StartGame(string username, string roomId)
        {
            throw new NotImplementedException();
        }

        public Room UnmarkReady(string username, string roomId)
        {
            return _roomMaster.UnmarkReady(username, roomId);
        }

        public Room UserDisconnected(string username)
        {
            return _roomMaster.UserDisconnected(username);
        }
    }
}
