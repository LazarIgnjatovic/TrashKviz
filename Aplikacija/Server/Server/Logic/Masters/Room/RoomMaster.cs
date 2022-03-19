using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Masters.Room
{
    public class RoomMaster : IRoomMaster
    {
        private Room[] activeRooms;

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
    }
}
