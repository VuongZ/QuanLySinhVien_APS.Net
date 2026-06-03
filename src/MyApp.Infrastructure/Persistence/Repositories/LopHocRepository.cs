using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Domain.Repositories;

namespace MyApp.Infrastructure.Persistence.Repositories;
public class LopHocRepository : Repository<LopHoc> , ILopHocRepository
{
    public LopHocRepository(AppDbContext appDbContex):base (appDbContex)
    {
    }
    public override async Task DeleteAsync(int id)
    {
       var lophoc=await GetByIdAsync(id);
       if(lophoc!=null)
        {
             _dbSet.Remove(lophoc);
            await _context.SaveChangesAsync();
        }
    }
    public override async Task<LopHoc?> GetByIdAsync(int id)
    {
       return await _dbSet.FirstOrDefaultAsync(lh=>lh.Id_LopHoc==id);
    }
    public async Task<LopHoc?> GetLopHocByTen(string tenLop)
    {
        return await _dbSet.FirstOrDefaultAsync(lh=>lh.TenLop==tenLop);
    }
}