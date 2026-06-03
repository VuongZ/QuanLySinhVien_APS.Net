namespace MyApp.Domain.Events;

public class SinhVienCreatedEvent
{
    public int Id_SinhVien { get; set; }
    public string TenSinhVien { get; set; } = string.Empty;
    public string MaSoSinhVien { get; set; } = string.Empty;
    public List<LopHocInEvent> DanhSachLopHoc { get; set; } =  new();
}
public class LopHocInEvent
{
    public int Id_LopHoc { get; set; }
    public string TenLop { get; set; } = string.Empty;
    public string Phong { get; set; } = string.Empty;
}