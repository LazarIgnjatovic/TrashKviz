using Microsoft.AspNetCore.Mvc;
using Server.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Services
{
    public interface IAuthenticationService
    {
        Task<ActionResult<string>> Login(LoginDTO loginDTO);
        Task<ActionResult<string>> Logout();
        Task<ActionResult<string>> Register(RegisterDTO registerDTO);

    }
}
