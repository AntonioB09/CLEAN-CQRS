using Infrastructure.Persistence.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Persistence
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
          
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<UserReadModel> Users => _database.GetCollection<UserReadModel>("Users");
       


    }
}


