using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Server.Logic;
using Server.Logic.Masters.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Communication.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LobbyHub:Hub
    {
        private readonly IRoomMaster _roomMaster;
        public LobbyHub(IRoomMaster roomMaster)
        {
            _roomMaster = roomMaster;
        }

        internal async Task RoomsUpdate(Room[] rooms)
        {
            //server interno poziva ovu funkciju
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
