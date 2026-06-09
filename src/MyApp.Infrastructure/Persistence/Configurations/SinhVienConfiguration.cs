using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Domain.Entities;

namespace MyApp.Infrastructure.Persistence.Configurations;
public class SinhVienConfiguration : IEntityTypeConfiguration<SinhVien>
{
    public void Configure(EntityTypeBuilder<SinhVien> builder)
    {
        builder.ToTable("SinhVien");
        builder.HasKey(sv=> sv.Id);
        builder.Property(sv=>sv.Id).HasColumnName("Id_SinhVien").ValueGeneratedOnAdd();
        builder.Property(sv=> sv.TenSinhVien).IsRequired().HasMaxLength(30);
        builder.Property(sv=>sv.MaSoSinhVien).IsRequired().HasMaxLength(10);
   
    }
}