using MongoDB.Driver;
using MyApp.Domain.Documents;
using MyApp.Domain.Repositories;
using MyApp.Infrastructure.MongoDB.RepoSitories;

namespace MyApp.Infrastructure.MongoDB.Repositories;

public class SinhVienMongoRepository : MonGoRepository<SinhVienDocument>, ISinhVienMongoRepository
{
    public SinhVienMongoRepository(MongoDbContext context) 
        : base(context, "SinhViens")
    {
    }

    public override async Task<SinhVienDocument?> GetByIdAsync(int id)
    {
        return await _collection
            .Find(sv => sv.IdSinhVien == id)
            .FirstOrDefaultAsync();
    }

    public override async Task UpdateAsync(int id, SinhVienDocument document)
    {
        await _collection.ReplaceOneAsync(
            sv => sv.IdSinhVien == id, document);
    }

    public override async Task DeleteAsync(int id)
    {
        await _collection.DeleteOneAsync(sv => sv.IdSinhVien == id);
    }

    public async Task<SinhVienDocument?> GetByMSSVAsync(string mssv)
    {
        return await _collection
            .Find(sv => sv.MaSoSinhVien == mssv)
            .FirstOrDefaultAsync();
    }
}