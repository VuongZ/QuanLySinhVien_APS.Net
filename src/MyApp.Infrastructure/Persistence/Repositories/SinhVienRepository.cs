using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Domain.Repositories;

namespace MyApp.Infrastructure.Persistence.Repositories;
public class SinhVienRepository : Repository<SinhVien> , ISinhVienRepository
{
    public SinhVienRepository(AppDbContext context):base (context)
    {
    }
    public override async Task<SinhVien?> GetByIdAsync(int id)
    {
        return await  _dbSet.FirstOrDefaultAsync(sv=> sv.Id_SinhVien == id);
    }
    public override async Task DeleteAsync(int id)
    {
         var entity = await  GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
             await _context.SaveChangesAsync();
        }
    }
    public async Task<SinhVien?> GetSinhVienByMSSV(string mssv)
    {
         return await _dbSet
            .FirstOrDefaultAsync(x => x.MaSoSinhVien == mssv);
    }
}