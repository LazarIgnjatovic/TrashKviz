using Newtonsoft.Json;
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
        public static int maxPlayers = 4;

        public string roomId;
        public string roomName;
        public bool isPublic;
        public List<RoomUserDTO> users;
        public GameType game1;
        public GameType game2;
        public GameType game3;

        [JsonIgnore]
        public string RoomID { get => roomId; set => roomId = value; }

        public Room() { }
        public Room(string id,UserDTO host)
        {
            roomId = id;
            isPublic = true;
            users = new List<RoomUserDTO>();
            RoomUserDTO admin = new RoomUserDTO(host);
            admin.isAdmin = true;
            users.Add(admin);
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
                if(!users.Exists(x=>x.User.Username==newPlayer.Username))
                    users.Add(new RoomUserDTO(newPlayer));
                return this;
            }
            else
                return null;
        }
        internal string RemovePlayer(string username)
        {
            RoomUserDTO user = users.Where(x => x.User.Username == username).FirstOrDefault();
            if(user!=null)
            {
                users.Remove(user);
                if (user.isAdmin)
                {
                    if (users.Count > 0)
                    {
                        if(users[0]!=null)
                        {
                            users[0].isAdmin = true;
                            return users[0].User.Username;
                        }   
                    }
                }
            }
            return null;
        }
        internal void MarkReady(string username)
        {
            RoomUserDTO user = users.Where(x => x.User.Username == username).FirstOrDefault();
            if (user != null)
                user.IsReady = !user.IsReady;
        }
        internal List<string> Modify(Room newRoom)
        {
            List<string> kicked = new List<string>();
            this.isPublic = newRoom.isPublic;
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

        internal void Kick(string username)
        {
            users.Remove(users.Where(x => x.User.Username == username).FirstOrDefault());
        }
    }
}
