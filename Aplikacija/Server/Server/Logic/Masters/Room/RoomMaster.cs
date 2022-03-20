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

        void IRoomMaster.AddToQueue(string connectionId)
        {
            queue.Enqueue(connectionId);
        }

        void IRoomMaster.RemoveFromQueue(string connectionId)
        {
            queue = new Queue<string>(queue.Where(x => x != connectionId));
        }

        Room IRoomMaster.CreateRoom()
        {
            throw new NotImplementedException();
        }
    }
}
