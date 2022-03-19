using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Server.Persistence.DatabaseSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Persistence.Models
{
    [BsonCollection("Questions")]
    [BsonKnownTypes(typeof(MultipleChoice), typeof(Association), typeof(ClosestNumber), typeof(StepByStep))]
    public class Question : IDocument
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public int Points { get; set; }
    }
}
