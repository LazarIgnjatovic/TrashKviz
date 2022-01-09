using Server.Logic.Business.DTOs;
using Server.Logic.Business.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Business
{
    public class Room
    {
        private bool isPublic;
        private UserDTO[] users;
        private GameType game1;
        private GameType game2;
        private GameType game3;
    }
}
