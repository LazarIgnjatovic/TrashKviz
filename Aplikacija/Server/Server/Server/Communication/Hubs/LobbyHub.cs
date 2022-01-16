using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Server.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Communication.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LobbyHub:Hub
    {
        internal async Task RoomsUpdate(Room[] rooms)
        {
            //server interno poziva ovu funkciju
            //temp resenje
            await Clients.All.SendAsync("UpdateRooms", rooms);
        }
        public async Task FindRoom()
        {
            //roomID je ujedno i kod za pristup sobi
            string roomID = "";
            //todo pronalazenje sobe
            await Clients.Caller.SendAsync("RoomFound", roomID);
        }
        public async Task Logout()
        {
            //todo logout
            await Clients.Caller.SendAsync("LoggedOut");
        }
    }
}
