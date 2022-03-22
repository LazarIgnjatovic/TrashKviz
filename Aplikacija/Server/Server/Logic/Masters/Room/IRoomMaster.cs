using Server.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Masters.Room
{
    public interface IRoomMaster
    {
        Room FindRoom(string id);
        Room[] FreeRooms();
        void AddToQueue(string connectionId);
        void RemoveFromQueue(string connectionId);
        Room CreateRoom(UserDTO host);
        Room JoinRoom(UserDTO user, string id);
        Room LeaveRoom(string username, string id);
        Room MarkReady(string username, string id);
        Room UnmarkReady(string username, string id);
        Room ModifyRoom(string username, Room room);
        Room UserDisconnected(string username);
        void PopulateRoom(Room room);
    }
}
