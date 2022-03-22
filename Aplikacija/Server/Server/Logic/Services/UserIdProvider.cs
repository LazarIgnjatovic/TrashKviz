using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Server.Logic.Services
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.GetHttpContext().User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
        }
    }
}
