namespace MyApp.Domain.Events;
public class SinhVienUpdatedEvent
{
    public int Id_SinhVien { get; set; }
    public string TenSinhVien { get; set; } = string.Empty;
    public string MaSoSinhVien { get; set; } = string.Empty;
}