using Server.Logic.DTOs;
using Server.Logic.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Masters.Room
{
    public class Room
    {
        private static int maxPlayers=4;

        private string roomId;
        private bool isPublic;
        private UserDTO[] users;
        private GameType game1;
        private GameType game2;
        private GameType game3;

        public string RoomID { get => roomId; set => roomId = value; }

        public Room() { }
        public Room(string id,UserDTO host)
        {
            roomId = id;
            isPublic = true;
            users[0] = host;
        }
        internal bool IsFree()
        {
            return users.Length < maxPlayers && isPublic;
        }
    }
}
