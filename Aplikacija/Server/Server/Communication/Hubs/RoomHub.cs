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
    public class RoomHub:Hub
    {
        private readonly IRoomMaster _roomMaster;
        public RoomHub(IRoomMaster roomMaster)
        {
            _roomMaster = roomMaster;
        }

        public async Task JoinRoom(string roomID)
        {
            //todo pristup sobi
            Room room = new Room();
            bool roomFull = false;
            if (roomFull)
                await Clients.Caller.SendAsync("RoomFull");
            else
            {
                //todo
                await Groups.AddToGroupAsync(Context.ConnectionId, roomID);
                await Clients.Group(roomID).SendAsync("RoomUpdate", room);
            }
        }
        public async Task LeaveRoom(string roomID)
        {
            //todo leave room
            Room room = new Room();
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomID);
            await Clients.Group(roomID).SendAsync("RoomUpdate", room);
        }
        public async Task ModifyRoom(Room room)
        {
            //todo modifikacija
            await Clients.Group(room.RoomID).SendAsync("RoomUpdate", room);
        }
        public async Task MarkReady(string roomID)
        {
            //todo 
            Room room = new Room();
            await Clients.Group(roomID).SendAsync("RoomUpdate", room);
        }
        public async Task StartGame(string roomID)
        {
            //todo start game
            string matchID="";
            await Clients.Group(roomID).SendAsync("GameStarted", matchID);
        }
    }
}
