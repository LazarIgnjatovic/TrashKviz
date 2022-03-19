using Microsoft.AspNetCore.SignalR;
using Server.Logic.Masters.Room;
using Server.Logic.Services;
using System.Threading.Tasks;

namespace Server.Communication.Hubs
{
    public class LobbyHub:Hub
    {
        private ILobbyService _lobbyService;
        public LobbyHub(ILobbyService lobbyService)
        {
            _lobbyService = lobbyService;
        }

        internal async Task RoomsUpdate(Room[] rooms)
        {
            //server interno poziva ovu funkciju
            await Clients.All.SendAsync("UpdateRooms", rooms);
        }

        public async Task GetRooms()
        {
            Room[] rooms = _lobbyService.GetRooms();
            await Clients.All.SendAsync("UpdateRooms", rooms);
        }
        public async Task FindRoom(string roomID)
        {
            Room room = _lobbyService.FindRoom(roomID);
            if (room != null)
                await Clients.Caller.SendAsync("RoomFound", roomID);
            else
                await Clients.Caller.SendAsync("RoomNotFound");
        }
        public async Task Logout()
        {
            //todo logout
            await Clients.Caller.SendAsync("LoggedOut");
        }
    }
}
