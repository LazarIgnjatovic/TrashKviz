using Server.Logic.DTOs;
using Server.Logic.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Masters.Room
{
    public class Room
    {
        public static int maxPlayers=4;

        public string roomId;
        public string roomName;
        public bool isPublic;
        public List<RoomUserDTO> users;
        public GameType game1;
        public GameType game2;
        public GameType game3;

        public string RoomID { get => roomId; set => roomId = value; }

        public Room() { }
        public Room(string id,UserDTO host)
        {
            roomId = id;
            isPublic = true;
            users = new List<RoomUserDTO>();
            users.Add(new RoomUserDTO(host));
            roomName = "Kod " + host.Username;

        }
        internal bool IsFree()
        {
            return users.Count < maxPlayers && isPublic;
        }
        internal int FreeSlots()
        {
            return maxPlayers - users.Count;
        }
        internal Room AddPlayer(UserDTO newPlayer)
        {
            if (users.Count < maxPlayers)
            {
                users.Append(new RoomUserDTO(newPlayer));
                return this;
            }
            else
                return null;
        }
        internal Room RemovePlayer(string username)
        {
            users.Remove(users.Where(x => x.User.Username == username).FirstOrDefault());
            return this;
        }
        internal void MarkReady(string username)
        {
            RoomUserDTO user = users.Where(x => x.User.Username == username).FirstOrDefault();
            if (user != null)
                user.IsReady = true;
        }
        internal void UnmarkReady(string username)
        {
            RoomUserDTO user = users.Where(x => x.User.Username == username).FirstOrDefault();
            if (user != null)
                user.IsReady = false;
        }
        internal List<string> Modify(Room newRoom)
        {
            List<string> kicked = new List<string>();
            foreach(RoomUserDTO currUser in users)
            {
                if (!newRoom.users.Exists(x => x.User.Username == currUser.User.Username))
                {
                    kicked.Add(currUser.User.Username);
                }
            }
            this.isPublic = newRoom.isPublic;
            this.users = newRoom.users;
            this.roomName = newRoom.roomName;
            this.game1 = newRoom.game1;
            this.game2 = newRoom.game2;
            this.game3 = newRoom.game3;
            return kicked;
        }
        internal bool HasUser(string username)
        {
            foreach(RoomUserDTO u in users)
                if (u.User.Username == username)
                    return true;
            return false;
        }
    }
}
