using Microsoft.AspNetCore.SignalR;
using Server.Communication.Hubs;
using Server.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Masters.Room
{
    public class RoomMaster : IRoomMaster
    {
        private List<Room> activeRooms;
        private Queue<string> queue;

        private readonly IHubContext<LobbyHub> _hubContext;

        public RoomMaster(IHubContext<LobbyHub> hubContext)
        {
            _hubContext = hubContext;
            queue = new Queue<string>();
        }

        public Room FindRoom(string id)
        {
            foreach(Room room in activeRooms)
            {
                if (room.RoomID == id)
                    return room;
            }
            return null;
        }

        public Room[] FreeRooms()
        {
            Room[] rooms= { };
            foreach(Room room in activeRooms)
            {
                if (room.IsFree())
                    rooms.Append(room);
            }
            return rooms;
        }

        public void AddToQueue(string connectionId)
        {
            queue.Enqueue(connectionId);
        }

        public void RemoveFromQueue(string connectionId)
        {
            queue = new Queue<string>(queue.Where(x => x != connectionId));
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
            activeRooms.Append(room);
            _hubContext.Clients.All.SendAsync("UpdateRooms", FreeRooms());
            PopulateRoom(room);
            return room;
        }
        private string GenerateCode(int codeSize)
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
                    string connId = queue.Dequeue();
                    _hubContext.Clients.Client(connId).SendAsync("RoomFound", room.RoomID);
                }
            }
        }
        public Room JoinRoom(UserDTO user, string id)
        {
            Room r=activeRooms.Where(x => x.RoomID == id).FirstOrDefault();
            if (r != null)
            {
                return r.AddPlayer(user);
            }
            else
                return null;
        }

        public Room LeaveRoom(string username, string id)
        {
            Room r = activeRooms.Where(x => x.RoomID == id).FirstOrDefault();
            r.RemovePlayer(username);
            if (r.users.Count() == 0)
                activeRooms.Remove(r);
            return r;
        }

        public Room MarkReady(string username, string id)
        {
            Room r = activeRooms.Where(x => x.RoomID == id).FirstOrDefault();
            r.MarkReady(username);
            return r;
        }

        public Room UnmarkReady(string username, string id)
        {
            Room r = activeRooms.Where(x => x.RoomID == id).FirstOrDefault();
            r.UnmarkReady(username);
            return r;
        }

        public Room ModifyRoom(string username, Room room)
        {
            
            Room r = activeRooms.Where(x => x.RoomID == room.RoomID).FirstOrDefault();
            if (r!=null && username==r.users[0].User.Username)
            {
                r.Modify(room);
                if (r.IsFree())
                    PopulateRoom(r);
            }
            return r;
        }

        public Room UserDisconnected(string username)
        {
            Room r = activeRooms.Where(x => x.HasUser(username)).FirstOrDefault();
            if(r!=null)
            {
                r.RemovePlayer(username);
                if (r.users.Count() == 0)
                    activeRooms.Remove(r);
            }
            return r;
        }
    }
}
