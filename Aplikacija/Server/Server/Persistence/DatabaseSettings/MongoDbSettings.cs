using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Persistence.DatabaseSettings
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

    }
}
