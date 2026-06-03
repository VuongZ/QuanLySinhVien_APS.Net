namespace MyApp.Domain.Events;
public class LopHocUpdatedEvent
{
    public int Id_LopHoc { get; set; }
    public string TenLop { get; set; } = string.Empty;
    public string Phong { get; set; } = string.Empty;


}