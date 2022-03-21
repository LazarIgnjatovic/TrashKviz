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
        private Room[] activeRooms;
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
            for(int i=0;i<activeRooms.Length;i++ )
            {
                if(activeRooms[i].RoomID==code)
                {
                    code = GenerateCode(4);
                    i = 0;
                }
            }
            Room room = new Room(code,host);
            return room;
        }
        private string GenerateCode(int codeSize)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, codeSize)
              .Select(s => s[random.Next(chars.Length)]).ToArray());
        }
    }
}
