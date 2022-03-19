using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Persistence.Models
{
    [BsonIgnoreExtraElements]
    public class Stats
    {
        public int GamesPlayed { get; set; }
        public float Winrate { get; set; }
    }
}
