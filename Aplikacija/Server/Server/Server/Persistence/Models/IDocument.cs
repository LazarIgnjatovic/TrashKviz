using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Persistence.Models
{
    public interface IDocument
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}
