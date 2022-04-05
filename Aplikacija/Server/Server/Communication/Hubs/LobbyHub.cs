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

        public LobbyHub(ILobbyService lobbyService)
        {
            _lobbyService = lobbyService;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _lobbyService.RemoveFromQueue(Context.UserIdentifier);
            return base.OnDisconnectedAsync(exception);
        }
        public override Task OnConnectedAsync()
        {
            if (_lobbyService.CheckReconnect(Context.UserIdentifier))
                Clients.Caller.SendAsync("Reconnect");
            return base.OnConnectedAsync();
        }

        public async Task GetRooms()
        {
            List<RoomDTO> rooms = _lobbyService.GetRooms();
            await Clients.All.SendAsync("UpdateRooms", rooms);
        }
        public async Task JoinRoom(string roomID)
        {
            Room room = _lobbyService.FindRoom(roomID,Context.UserIdentifier);
            if (room != null)
                await Clients.Caller.SendAsync("RoomFound");
            else
                await Clients.Caller.SendAsync("RoomNotFound");
        }
        public void FindRoom()
        {
            _lobbyService.AddToQueue(Context.UserIdentifier);
        }
        public void CancelSearch()
        {
            _lobbyService.RemoveFromQueue(Context.UserIdentifier);
        }
        public async Task CreateRoom()
        {
            string roomId =_lobbyService.CreateRoom(Context.UserIdentifier);
            await Clients.Caller.SendAsync("RoomFound");
        }
        //await Clients.Caller.SendAsync("RoomFull");
    }
}
