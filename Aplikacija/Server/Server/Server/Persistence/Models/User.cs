using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Server.Persistence.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        public string Username { get; set; }
        public string PassHash { get; set; }
        public string Email { get; set; }
        public Stats Stats { get; set; }

    }
}
