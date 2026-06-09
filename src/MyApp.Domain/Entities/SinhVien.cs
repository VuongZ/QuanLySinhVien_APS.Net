namespace MyApp.Domain.Entities;

public class SinhVien : BaseId<int>
{
    public string TenSinhVien { get; set; } = string.Empty;
    public string MaSoSinhVien { get; set; } = string.Empty;


}