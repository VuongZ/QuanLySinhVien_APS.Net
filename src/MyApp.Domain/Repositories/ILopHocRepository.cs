using MyApp.Domain.Entities;

namespace MyApp.Domain.Repositories;
public interface ILopHocRepository : IRepository<LopHoc>
{
    Task<LopHoc?> GetByIdAsync(int id);
    Task<LopHoc?> GetLopHocByTen(string tenLop);
}