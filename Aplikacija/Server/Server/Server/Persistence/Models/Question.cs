using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Persistence.Models
{
    [BsonKnownTypes(typeof(MultipleChoice), typeof(Association))]
    public class Question
    {
        public int Points { get; set; }
    }
}
