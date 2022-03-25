using Microsoft.AspNetCore.SignalR;
using Server.Communication.Hubs;
using Server.Logic.DTOs;
using Server.Persistence.Models;
using Server.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Masters.Room
{
    public class RoomMaster : IRoomMaster
    {
        private List<Room> activeRooms;
        private Queue<UserDTO> queue;

        private readonly IHubContext<LobbyHub> _lobbyHubContext;
        private readonly IHubContext<RoomHub> _roomHubContext;

        public RoomMaster(IHubContext<LobbyHub> hubContext, IHubContext<RoomHub> roomHubContext)
        {
            _lobbyHubContext = hubContext;
            _roomHubContext = roomHubContext;
            queue = new Queue<UserDTO>();
            activeRooms = new List<Room>();
        }

        public Room FindRoom(string id, UserDTO user)
        {
            foreach(Room room in activeRooms)
            {
                if (room.RoomID == id&& room.FreeSlots()>0)
                {
                    if(!room.users.Exists(x=>x.User.Username==user.Username))
                    {
                        room.AddPlayer(user);
                        LobbyUpdate();
                    }
                    return room;
                }
                    
            }
            return null;
        }

        public List<Room> FreeRooms()
        {
            List<Room> rooms= new List<Room>();
            foreach(Room room in activeRooms)
            {
                if (room.IsFree())
                    rooms.Add(room);
            }
            return rooms;
        }

        public void AddToQueue(UserDTO user)
        {
            List<Room> rooms = FreeRooms();
            if (rooms.Count > 0)
            {
                rooms.FirstOrDefault().AddPlayer(user);
                _lobbyHubContext.Clients.User(user.Username).SendAsync("RoomFound");
            }
            else
                queue.Enqueue(user);
        }

        public void RemoveFromQueue(string connectionId)
        {
            queue = new Queue<UserDTO>(queue.Where(x => x.Username != connectionId));
        }

        public Room CreateRoom(UserDTO host)
        {
            string code = GenerateCode(4);
            for(int i=0;i<activeRooms.Count;i++ )
            {
                if(activeRooms[i].RoomID==code)
                {
                    code = GenerateCode(4);
                    i = 0;
                }
            }
            Room room = new Room(code,host);
            activeRooms.Add(room);
            LobbyUpdate();
            PopulateRoom(room);
            return room;
        }
        public string GenerateCode(int codeSize)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, codeSize)
              .Select(s => s[random.Next(chars.Length)]).ToArray());
        }
        public void PopulateRoom(Room room)
        {
            for(int i=0;i<room.FreeSlots();i++)
            {
                if(queue.Count>0)
                {
                    UserDTO user = queue.Dequeue();
                    room.AddPlayer(user);
                    _lobbyHubContext.Clients.User(user.Username).SendAsync("RoomFound");
                    LobbyUpdate();
                }
            }
            CheckReady(room);
        }
        public Room LeaveRoom(string username, string id)
        {
            Room r = activeRooms.Where(x => x.RoomID == id).FirstOrDefault();
            string newAdmin = r.RemovePlayer(username);
            if(newAdmin!=null)
            {
                _roomHubContext.Clients.User(newAdmin).SendAsync("PromoteToAdmin");
            }
            if (r.users.Count() == 0)
                activeRooms.Remove(r);
            else if (r.IsFree())
            {
                PopulateRoom(r);
            }
            CheckReady(r);
            return r;
        }

        public Room MarkReady(string username, string id)
        {
            Room r = activeRooms.Where(x => x.RoomID == id).FirstOrDefault();
            bool wereReady = true;
            foreach (RoomUserDTO user in r.users)
            {
                if (!user.IsReady)
                    wereReady = false;
            }
            r.MarkReady(username);
            bool allReady = true;
            string admin="";
            foreach (RoomUserDTO user in r.users)
            {
                if (!user.IsReady)
                    allReady = false;
                if(user.isAdmin)
                {
                    admin = user.User.Username;
                }

            }
            if (allReady)
                _roomHubContext.Clients.User(admin).SendAsync("AllReady");
            else if (wereReady)
                _roomHubContext.Clients.User(admin).SendAsync("NotAllReady");
            return r;
        }

        public Room ModifyRoom(string username, Room room)
        {
            
            Room r = activeRooms.Where(x => x.RoomID == room.RoomID).FirstOrDefault();
            if (r!=null && username==r.users[0].User.Username)
            {
                r.Modify(room);
                if (r.IsFree())
                {
                    PopulateRoom(r);
                    if (r.roomName != room.roomName || r.users.Count != room.users.Count)
                        LobbyUpdate();
                }
            }
            return r;
        }

        public Room UserDisconnected(string username)
        {
            Room r = activeRooms.Where(x => x.HasUser(username)).FirstOrDefault();
            if(r!=null)
            {
                string newAdmin = r.RemovePlayer(username);
                if (newAdmin != null)
                {
                    _roomHubContext.Clients.User(newAdmin).SendAsync("PromoteToAdmin");
                }
                if (r.users.Count() == 0)
                    activeRooms.Remove(r);
                else if(r.users.Count<Room.maxPlayers)
                {
                    PopulateRoom(r);
                    LobbyUpdate();
                }
                CheckReady(r);
            }
            return r;
        }

        public Room ConnectedUser(string username)
        {
            return activeRooms.Where(x => x.users.Exists(user => user.User.Username == username)).FirstOrDefault();
        }

        public void GameStarted(Room room)
        {
            activeRooms.Remove(room);
        }

        public void Kick(string roomID, string username, string userIdentifier)
        {
            Room r = activeRooms.Where(x => x.RoomID == roomID).FirstOrDefault();
            if (r != null && userIdentifier == r.users[0].User.Username)
            {
                r.Kick(username);
                _roomHubContext.Clients.User(username).SendAsync("Kicked");
                RoomUpdate(roomID, r);
                CheckReady(r);
                LobbyUpdate();
            }
        }

        private void RoomUpdate(string roomID, Room r)
        {
            _roomHubContext.Clients.Group(roomID).SendAsync("RoomUpdate", r);
        }

        public void CheckReady(Room r)
        {
            bool allReady = true;
            string admin = "";
            foreach (RoomUserDTO user in r.users)
            {
                if (!user.IsReady)
                    allReady = false;
                if (user.isAdmin)
                {
                    admin = user.User.Username;
                }

            }
            if (allReady)
                _roomHubContext.Clients.User(admin).SendAsync("AllReady");
            else
                _roomHubContext.Clients.User(admin).SendAsync("NotAllReady");
        }

        private void LobbyUpdate()
        {
            List<Room> rooms = FreeRooms();
            List<RoomDTO> roomsList = new List<RoomDTO>();
            foreach (Room r in rooms)
            {
                roomsList.Add(new RoomDTO(r));
            }
            _lobbyHubContext.Clients.All.SendAsync("UpdateRooms", roomsList);
        }
    }
}
