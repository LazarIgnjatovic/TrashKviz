using Server.Logic.DTOs;
using Server.Logic.Masters.Match;
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
        private readonly IMatchMaster _matchMaster;

        public RoomService(IRoomMaster roomMaster, IBaseRepository<User> baseUserRepository,IMatchMaster matchMaster)
        {
            _roomMaster = roomMaster;
            _baseUserRepository = baseUserRepository;
            _matchMaster = matchMaster;
        }

        public void CheckReady(Room r)
        {
            _roomMaster.CheckReady(r);
        }

        public Room ConnectedUser(string username)
        {
            return _roomMaster.ConnectedUser(username);
        }

        public void Kick(string roomID, string username, string userIdentifier)
        {
            _roomMaster.Kick(roomID, username, userIdentifier);
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
            User u = _baseUserRepository.FindOne(x => x.Username == username);
            UserDTO user = new UserDTO(u);
            Room r = _roomMaster.FindRoom(roomId,user);
            if(r.users.Where(x=>x.isAdmin).FirstOrDefault().User.Username==username)
            {
                bool allReady = true;
                foreach(RoomUserDTO player in r.users)
                {
                    if (!player.IsReady)
                        allReady = false;
                }
                if(allReady)
                {
                    while (_matchMaster.MatchCodeExists(roomId))
                    {
                        r.roomId = _roomMaster.GenerateCode(4);
                    }
                    _matchMaster.StartGame(r);
                    _roomMaster.GameStarted(r);
                    return true;
                }
                return false;
            }
            return false;
        }
        public Room UserDisconnected(string username)
        {
            return _roomMaster.UserDisconnected(username);
        }
    }
}
