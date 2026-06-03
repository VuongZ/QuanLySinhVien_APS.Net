namespace MyApp.Domain.Events;

public class LopHocCreatedEvent
{
    public int Id_LopHoc { get; set; }
    public string TenLop { get; set; } = string.Empty;
    public string Phong { get; set; } = string.Empty;
    public List<SinhVienInLopEvent> DanhSachSinhVien { get; set; }=new();
}
public class SinhVienInLopEvent
{
    public int Id_SinhVien { get; set; }
    public string TenSinhVien { get; set; } = string.Empty;
    public string MaSoSinhVien { get; set; } = string.Empty;
}