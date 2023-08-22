using MongoDB.Driver;

namespace EY_Project.Infrastructure.Repositories;

public interface IMongoRepository
{
    Task<List<T>> GetAllDocuments<T>(string dbName, string collectionName);
    Task<List<T>> GetFilteredDocuments<T>(string name, string collectionName, FilterDefinition<T> filter);
    Task UpdateDocument<T>(string dbName, string collectionName, FilterDefinition<T> filter, UpdateDefinition<T> document);
    Task CreateDocument<T>(string dbName, string collectionName, T document);
    Task DeleteDocument<T>(string dbName, string collectionName, FilterDefinition<T> filter);

}
