
using MyApp.Domain.Documents;

namespace MyApp.Domain.Repositories;

public interface ISinhVienMongoRepository : IMongoRepository<SinhVienDocument>
{
    Task<SinhVienDocument?> GetByMSSVAsync(string mssv);
}