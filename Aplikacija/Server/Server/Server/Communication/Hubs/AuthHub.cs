using Microsoft.AspNetCore.SignalR;
using Server.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Communication.Hubs
{
    public class AuthHub: Hub
    {
        public async Task Login(string username, string password)
        {
            UserDTO user=new UserDTO();
            string token = "";
            //todo
            await Clients.Caller.SendAsync("LoginSuccess", user, token);
        }
        public async Task Register(string username, string password, string email)
        {
            //todo registracija
            await Login(username, password);
        }
    }
}
