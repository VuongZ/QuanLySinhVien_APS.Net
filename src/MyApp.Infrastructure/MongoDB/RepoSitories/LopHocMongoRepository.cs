using Microsoft.IdentityModel.Logging;
using MongoDB.Driver;
using MyApp.Domain.Documents;
using MyApp.Domain.Repositories;

namespace MyApp.Infrastructure.MongoDB.RepoSitories;
public class LopHocMongoRepository : MonGoRepository<LopHocDocument> , ILopHocMongoRepository
{
    public LopHocMongoRepository(MongoDbContext mongoDbContext) : base (mongoDbContext,"Lop Hoc")
    {
        
    }
     public override async Task<LopHocDocument?> GetByIdAsync (int id)
    {
        return await _collection.Find(lh => lh.IdLopHoc==id).FirstOrDefaultAsync();
            
    }

    public override async Task UpdateAsync (int id, LopHocDocument document)
    {
        await _collection.ReplaceOneAsync(l=> l.IdLopHoc==id,document);
    }

    public override async Task DeleteAsync(int id)
    {
        await _collection.DeleteOneAsync(sv => sv.IdLopHoc == id);
    }

    public async Task<LopHocDocument?> GetByTenLopAsync(string tenlop)
    {
        return await _collection
            .Find(lh => lh.TenLop == tenlop)
            .FirstOrDefaultAsync();
    }
}