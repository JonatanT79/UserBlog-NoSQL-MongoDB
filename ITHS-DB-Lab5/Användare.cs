using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ITHS_DB_Lab5
{
    class Användare
    {
        [BsonId]
        public Guid ID { get; set; }
        public string Förnamn { get; set; }
        public string Efternamn { get; set; }
    }

}
//4
// lägg in hemsidan i en metod och anropa -- behöver inte köra programmet hela tiden
//felhantering i t.ex 6 med guid om man anger en string elr int
