﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Logic.DTOs;
using Server.Persistence.Models;
using Server.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Server.Logic.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBaseRepository<User> _baseRepository;

        public AuthenticationService(IHttpContextAccessor httpContextAccessor, IBaseRepository<User> baseRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _baseRepository = baseRepository;
        }
        public async Task<ActionResult<string>> Login(LoginDTO loginDTO)
        {
            var user = await _baseRepository.FindOneAsync(user => user.Username.Equals(loginDTO.Username));
            if (user == null || !BCrypt.Net.BCrypt.EnhancedVerify(loginDTO.Password, user.PassHash)) 
                return new UnauthorizedObjectResult("Moguće li je da ne umeš da se uloguješ?!");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, loginDTO.Username)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,

            };

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                               new ClaimsPrincipal(claimsIdentity));

            return new OkObjectResult("Uspešan ulog!");
        }

        public async Task<ActionResult<string>> Logout()
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return new UnauthorizedObjectResult("Šefe obično treba da si ulogovan da bi se izlogovao!");
            await _httpContextAccessor.HttpContext.SignOutAsync();
            return new OkObjectResult("Uspešan izlog!");
        }

        public async Task<ActionResult<string>> Register(RegisterDTO registerDTO)
        {
            var existingUser = await _baseRepository.AnyAsync(user => user.Username.Equals(registerDTO.Username)
                                                                      || user.Email.Equals(registerDTO.Email));

            if (!existingUser) return new BadRequestObjectResult("Postoji nalog sa prosleđenim korisničkim imenom ili e-mail-om. Na tebi je da provališ koje da promeniš, mene ne plaćaju dovoljno za to.");

            var newUser = new User()
            {
                Username = registerDTO.Username,
                PassHash = BCrypt.Net.BCrypt.EnhancedHashPassword(registerDTO.Password),
                Email = registerDTO.Email,
                Stats = new Stats()
            };

            await _baseRepository.InsertOneAsync(newUser);

            return new OkObjectResult("Alal ti ćufta, uspešna registracija!");
        }
    }
}
