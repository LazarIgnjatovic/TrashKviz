using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Server.Logic.Masters.Room;
using Server.Logic.Services;
using System;
using System.Threading.Tasks;

namespace Server.Communication.Hubs
{ 
    [Authorize]
    public class LobbyHub:Hub
    {
        private ILobbyService _lobbyService;
        public LobbyHub(ILobbyService lobbyService)
        {
            _lobbyService = lobbyService;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _lobbyService.RemoveFromQueue(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task GetRooms()
        {
            Room[] rooms = _lobbyService.GetRooms();
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
    }
}
