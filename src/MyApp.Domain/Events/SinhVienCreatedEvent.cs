namespace MyApp.Domain.Events;

public class SinhVienCreatedEvent
{
    public int IdSinhVien { get; set; }
    public string TenSinhVien { get; set; } = string.Empty;
    public string MaSoSinhVien { get; set; } = string.Empty;
    public List<LopHocInEvent> DanhSachLopHoc { get; set; } =  new();
}
public class LopHocInEvent
{
    public int IdLopHoc { get; set; }
    public string TenLop { get; set; } = string.Empty;
    public string Phong { get; set; } = string.Empty;
}