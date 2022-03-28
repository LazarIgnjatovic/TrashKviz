using Server.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Masters.Room
{
    public interface IRoomMaster
    {
        Room FindRoom(string id,UserDTO user);
        List<Room> FreeRooms();
        void AddToQueue(UserDTO user);
        void RemoveFromQueue(string connectionId);
        Room CreateRoom(UserDTO host);
        Room LeaveRoom(string username, string id);
        Room MarkReady(string username, string id);
        Room ModifyRoom(string username, Room room);
        Room UserDisconnected(string username);
        void PopulateRoom(Room room);
        Room ConnectedUser(string username);
        void GameStarted(Room room);
        string GenerateCode(int codeSize);
        void Kick(string roomID, string username, string userIdentifier);
        void CheckReady(Room r);
        Room GetRoom(string roomId);
    }
}
