using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.DTOs
{
    public class RoomUserDTO
    {
        public UserDTO User;
        public bool IsReady;
        public bool isAdmin;

        public RoomUserDTO(UserDTO u)
        {
            User = u;
            IsReady = false;
            isAdmin = false;
        }
    }
}
