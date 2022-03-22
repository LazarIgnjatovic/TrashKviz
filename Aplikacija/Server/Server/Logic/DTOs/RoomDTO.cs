using Server.Logic.Masters.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.DTOs
{
    public class RoomDTO
    {
        public string roomId;
        public string roomName;
        public int numberOfPlayersJoined;

        public RoomDTO(Room r)
        {
            roomId = r.roomId;
            roomName = r.roomName;
            numberOfPlayersJoined = r.users.Count();
        }
    }
}
