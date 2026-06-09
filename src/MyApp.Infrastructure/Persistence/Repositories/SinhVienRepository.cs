using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Domain.Repositories;

namespace MyApp.Infrastructure.Persistence.Repositories;
public class SinhVienRepository : Repository<SinhVien> , ISinhVienRepository
{
    public SinhVienRepository(AppDbContext context):base (context)
    {
    }
  
}