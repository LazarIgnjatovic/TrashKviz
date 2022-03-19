using Microsoft.AspNetCore.SignalR;
using Server.Logic.DTOs;
using Server.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Communication.Hubs
{
    public class AuthHub: Hub
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthHub(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task Login(LoginDTO loginDTO)
        {
            UserDTO user=new UserDTO();
            string token = "";
            //todo
            await Clients.Caller.SendAsync("LoginSuccess", user, token);
        }
        public async Task Register(RegisterDTO registerDTO)
        {
            //todo registracija
            await Login(registerDTO);
        }
    }
}
