using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;    

namespace MyApp.Infrastructure.Persistence;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<SinhVien> SinhViens=> Set<SinhVien>();
    public DbSet<LopHoc> LopHocs => Set<LopHoc>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

       modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
       modelBuilder.Entity<LopHoc>().HasQueryFilter(e=>!e.IsDeleted);
       modelBuilder.Entity<SinhVien>().HasQueryFilter(e => !e.IsDeleted);
     
    }
}