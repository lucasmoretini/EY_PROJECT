using MongoDB.Driver;

namespace EY_Project.Infrastructure.Repositories
{
    public class MongoRepository : IMongoRepository
    {
        private const string connectionString = "mongodb://user:5Mb4MlsHtQVALnrf@ac-npcitno-shard-00-00.2sndhry.mongodb.net:27017,ac-npcitno-shard-00-01.2sndhry.mongodb.net:27017,ac-npcitno-shard-00-02.2sndhry.mongodb.net:27017/?replicaSet=atlas-vpcy1g-shard-0&ssl=true&authSource=admin";

        public IMongoDatabase GetMongoDbInstance(string dbName)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(dbName);
            return db;
        }

        public async Task CreateDocument<T>(string dbName, string collectionName, T document)
        {
            await GetCollection<T>(dbName, collectionName).InsertOneAsync(document);
        }

        public async Task DeleteDocument<T>(string dbName, string collectionName, FilterDefinition<T> filter)
        {
            await GetCollection<T>(dbName, collectionName).DeleteOneAsync(filter);
        }

        public async Task<List<T>> GetAllDocuments<T>(string dbName, string collectionName)
        {
            var collection = GetCollection<T>(dbName, collectionName);
            return await collection.Find(x => true).ToListAsync();
        }

        public async Task<List<T>> GetFilteredDocuments<T>(string dbName, string collectionName, FilterDefinition<T> filter) =>
            await GetCollection<T>(dbName, collectionName).Find(filter).ToListAsync();

        public async Task UpdateDocument<T>(string dbName, string collectionName, FilterDefinition<T> filter, UpdateDefinition<T> document)
        {
            await GetCollection<T>(dbName, collectionName).UpdateOneAsync(filter, document);
        }

        private IMongoCollection<T> GetCollection<T>(string dbName, string collectionName) =>
            GetMongoDbInstance(dbName).GetCollection<T>(collectionName);
    }
}
