using MyApp.Domain.Entities;

namespace MyApp.Domain.Repositories;
public interface ISinhVienRepository : IRepository<SinhVien>
{
    Task<SinhVien?> GetByIdAsync(int id);

    Task<SinhVien?> GetSinhVienByMSSV(string mssv);
}