using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Server.Logic;
using Server.Logic.Masters.Room;
using Server.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Communication.Hubs
{
    [Authorize]
    public class RoomHub:Hub
    {
        private readonly IRoomService _roomService;
        public RoomHub(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            string username = Context.UserIdentifier;
            Room room=_roomService.UserDisconnected(username);
            if (room != null)
            {
                Groups.RemoveFromGroupAsync(Context.ConnectionId, room.RoomID);
                Clients.Group(room.RoomID).SendAsync("RoomUpdate", room);
            }
            return base.OnDisconnectedAsync(exception);
        }

        public async Task JoinRoom(string roomID)
        {
            string username = Context.UserIdentifier;
            Room room = _roomService.JoinRoom(username, roomID);
            if (room==null)
                await Clients.Caller.SendAsync("RoomFull");
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, roomID);
                await Clients.Group(roomID).SendAsync("RoomUpdate", room);
            }
        }
        public async Task LeaveRoom(string roomID)
        {
            string username = Context.UserIdentifier;
            Room room = _roomService.LeaveRoom(username, roomID);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomID);
            await Clients.Group(roomID).SendAsync("RoomUpdate", room);
        }
        public async Task ModifyRoom(Room newRoom)
        {
            string username = Context.UserIdentifier;
            Room room = _roomService.ModifyRoom(username, newRoom);
            await Clients.Group(room.RoomID).SendAsync("RoomUpdate", room);
        }
        public async Task MarkReady(string roomID)
        {
            string username = Context.UserIdentifier;
            Room room = _roomService.MarkReady(username, roomID);
            await Clients.Group(roomID).SendAsync("RoomUpdate", room);
        }
        public async Task StartGame(string roomID)
        {
            //todo start game
            string matchID="";
            await Clients.Group(roomID).SendAsync("GameStarted", matchID);
        }
        //_roomHubContext.Clients.User(user).SendAsync("Kicked");
    }
}
