using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Server.Persistence.DatabaseSettings;

namespace Server.Persistence.Models
{
    [BsonCollection("Users")]
    [BsonIgnoreExtraElements]
    public class User : IDocument
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Username { get; set; }
        public string PassHash { get; set; }
        public string Email { get; set; }
        public Stats Stats { get; set; }

    }
}
