using Server.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.DTOs
{
    public class UserDTO
    {
        public string Username { get; set; }
        public Stats Stats { get; set; }

        public UserDTO(){}
        public UserDTO(User user)
        {
            this.Username = user.Username;
            this.Stats = user.Stats;
        }
    }
}
