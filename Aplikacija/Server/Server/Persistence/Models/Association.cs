using Server.Persistence.DatabaseSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Persistence.Models
{
    [BsonCollection("Questions")]
    public class Association : Question
    {
        public AssociationColumn[] Columns { get; set; }
        public string Answer { get; set; }
    }
}
