using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Server.Logic.Masters.Room;
using Server.Logic.Services;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using Server.Logic.DTOs;
using System.Collections.Generic;

namespace Server.Communication.Hubs
{ 
    [Authorize]
    public class LobbyHub:Hub
    {
        private readonly ILobbyService _lobbyService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserIdProvider _userIdProvider;

        public LobbyHub(ILobbyService lobbyService,IHttpContextAccessor httpContextAccessor, IUserIdProvider userIdProvider)
        {
            _lobbyService = lobbyService;
            _httpContextAccessor = httpContextAccessor;
            _userIdProvider = userIdProvider;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _lobbyService.RemoveFromQueue(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task GetRooms()
        {
            List<RoomDTO> rooms = _lobbyService.GetRooms();
            await Clients.All.SendAsync("UpdateRooms", rooms);
        }
        public async Task JoinRoom(string roomID)
        {
            Room room = _lobbyService.FindRoom(roomID);
            if (room != null)
                await Clients.Caller.SendAsync("RoomFound", roomID);
            else
                await Clients.Caller.SendAsync("RoomNotFound");
        }
        public async Task FindRoom()
        {
            _lobbyService.AddToQueue(Context.ConnectionId);
        }
        public async Task CreateRoom()
        {
            string roomId =_lobbyService.CreateRoom(Context.UserIdentifier);
            await Clients.Caller.SendAsync("RoomFound", roomId);
        }
    }
}
