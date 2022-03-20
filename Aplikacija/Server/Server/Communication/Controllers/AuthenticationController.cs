using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Logic.DTOs;
using Server.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Communication.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authentiactionService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authentiactionService = authenticationService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO loginDTO)
        {
            return await _authentiactionService.Login(loginDTO);
        }

        [HttpGet]
        [Route("Logout")]
        public async Task<ActionResult<string>> Logout()
        {
            return await _authentiactionService.Logout();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<string>> Register([FromBody] RegisterDTO registerDTO)
        {
            return await _authentiactionService.Register(registerDTO);
        }

        [HttpGet]
        [Route("IsLoggedIn")]
        [Authorize]
        public IActionResult IsLoggedIn()
        {
            return Ok();
        }
    }
}
