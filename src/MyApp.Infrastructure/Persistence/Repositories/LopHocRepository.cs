using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Domain.Repositories;

namespace MyApp.Infrastructure.Persistence.Repositories;
public class LopHocRepository : Repository<LopHoc> ,ILopHocRepository
{
       public LopHocRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
   
}