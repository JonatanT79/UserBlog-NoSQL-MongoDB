using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;

namespace ITHS_DB_Lab5
{
    class Mongoconnect
    {
        IMongoDatabase db;
        public Mongoconnect(string databas)
        {
            // ------------------------------------------------------------- Azure Cosmos ------------------------------------------------------

            //string connectionString =
            //@"mongodb://ithsdb2019cosmos:sihcAeAfo75o9z95exdpNlkgFUFMWqk6VIv73Ho1VRZ1oaodM7YWClWG7MJlumEPDACHPmrouI97Di5smE8XvQ==@ithsdb2019cosmos.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@ithsdb2019cosmos@";
            //MongoClientSettings settings = MongoClientSettings.FromUrl(
            //  new MongoUrl(connectionString)
            //);
            //settings.SslSettings =
            //  new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            //var mongoClient = new MongoClient(settings);
            //var client = new MongoClient(connectionString);
            //db = client.GetDatabase(databas);

            string conncectionstring = "mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass&ssl=false";
            var client = new MongoClient(conncectionstring);
            db = client.GetDatabase(databas);
        }
        public void AddBlogg<T>(string table, T record)
        {
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        public void RaderaBlogg<T>(string table, string Id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("ID", Id);
            collection.DeleteOne(filter);
        }
        public List<T> VisaBlogg<T>(string table)
        {
            var collection = db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();

        }
        public List<T> VisaKategori<T>(string table)
        {
            var collecion = db.GetCollection<T>(table);
            return collecion.Find(new BsonDocument()).ToList();
        }
        public List<T> VisaKategoriFilter<T>(string table, string kategori)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Kategori", kategori);
            return collection.Find(filter).ToList();
        }
        public List<T> VisaBloggpostFilter<T>(string table, string id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("ID", id);
            return collection.Find(filter).ToList();
        }
    }
}
