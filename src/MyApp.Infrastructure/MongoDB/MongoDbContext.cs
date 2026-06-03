using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MyApp.Contracts.Protos;
using MyApp.Domain.Documents;

namespace MyApp.Infrastructure.MongoDB;
public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    public MongoDbContext(IConfiguration configuration)
    {
        var connecstring = configuration.GetConnectionString("MongoDB");
        var databasename=configuration["MongoDB:Databasename"];
        var client= new MongoClient(connecstring);
        _database = client.GetDatabase(databasename);
    }
    public IMongoCollection<SinhVienDocument> SinhViens=>_database.GetCollection<SinhVienDocument>("Sinh Vien");
        public IMongoCollection<LopHocDocument> LopHocs=>_database.GetCollection<LopHocDocument>("Lop Hoc");
        public IMongoCollection<T> GetCollection<T>(string collectionName)
        => _database.GetCollection<T>(collectionName);
}