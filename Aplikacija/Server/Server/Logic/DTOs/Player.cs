using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.DTOs
{
    public class Player
    {
        public UserDTO User;
        public int Points;
        public bool IsConnected;

        public Player(RoomUserDTO user)
        {
            User = user.User;
            Points = 0;
            IsConnected = false;
        }
    }
}
