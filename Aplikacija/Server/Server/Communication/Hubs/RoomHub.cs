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
        private readonly IMatchService _matchService;
        public RoomHub(IRoomService roomService,IMatchService matchService)
        {
            _roomService = roomService;
            _matchService = matchService;
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

        public override Task OnConnectedAsync()
        {
            Room r = _roomService.ConnectedUser(Context.UserIdentifier);
            if (r != null)
            {
                if(r.users.Where(x=>x.User.Username==Context.UserIdentifier).FirstOrDefault().isAdmin)
                    Clients.Caller.SendAsync("PromoteToAdmin");
                Groups.AddToGroupAsync(Context.ConnectionId, r.roomId);
                
                Clients.Group(r.roomId).SendAsync("RoomUpdate", r);
                _roomService.CheckReady(r);
                //Room room = _roomService.ConnectedUser(Context.UserIdentifier);//!!!!
            }
            else
                Clients.Caller.SendAsync("RoomFull");
            return base.OnConnectedAsync();
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
            bool started=_roomService.StartGame(Context.UserIdentifier,roomID);
            if(started)
                await Clients.Group(roomID).SendAsync("GameStarted");
        }
        public async Task Kick(string roomID, string username)
        {
            _roomService.Kick(roomID, username, Context.UserIdentifier);
        }
        //_roomHubContext.Clients.User(user).SendAsync("Kicked");
        //_roomHubContext.Clients.User(admin).SendAsync("AllReady");
        //_roomHubContext.Clients.User(admin).SendAsync("NotAllReady");
    }
}
