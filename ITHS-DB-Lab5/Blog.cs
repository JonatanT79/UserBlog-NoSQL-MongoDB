using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ITHS_DB_Lab5
{
    class Blog
    {
        [BsonId]
        public Guid ID { get; set; }
        public string Rubrik { get; set; }
        public string Text { get; set; }
        public DateTime SkapadDatum { get; set; } = DateTime.Now;
        public string[] Kategori { get; set; }
    }

}
