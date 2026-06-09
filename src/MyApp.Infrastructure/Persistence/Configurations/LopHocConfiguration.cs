using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Domain.Entities;

namespace  MyApp.Infrastructure.Persistence.Configurations;
public class LopHocConfiguration : IEntityTypeConfiguration<LopHoc>
{
    public void Configure(EntityTypeBuilder<LopHoc> builder)
    {
        builder.ToTable("LopHoc");
        builder.HasKey(lh=>lh.Id);
        builder.Property(sv => sv.Id)
        .HasColumnName("Id_LopHoc")
        .ValueGeneratedOnAdd();
        builder.Property(lh=> lh.TenLop).IsRequired().HasMaxLength(100);
        builder.Property(lh=>lh.Phong).IsRequired().HasMaxLength(10);
    
    }
}