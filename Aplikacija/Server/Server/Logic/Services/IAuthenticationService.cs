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
        Task<ActionResult<ResponseDTO>> Login(LoginDTO loginDTO);
        Task<ActionResult<ResponseDTO>> Logout();
        Task<ActionResult<ResponseDTO>> Register(RegisterDTO registerDTO);
        ActionResult<bool> IsLoggedIn();
    }
}
