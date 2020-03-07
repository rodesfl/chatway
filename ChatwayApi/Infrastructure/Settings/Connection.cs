using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Settings {
    public class Connection : IConnection {
        public string Url { get; set; } = "mongodb://localhost:27017";
        public string Database { get; set; } = "chatway";

        public IMongoCollection<T> GetCollection<T>(string collection) {
            var client = new MongoClient(Url);
            var database = client.GetDatabase(Database);
            return database.GetCollection<T>(collection);
        }
    }

    public interface IConnection {
        string Url { get; set; }
        string Database { get; set; }

        public IMongoCollection<T> GetCollection<T>(string collection);
    }
}
