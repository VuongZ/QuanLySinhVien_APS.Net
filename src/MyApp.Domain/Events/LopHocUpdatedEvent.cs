namespace MyApp.Domain.Events;
public class LopHocUpdatedEvent
{
    public int IdLopHoc { get; set; }
    public string TenLop { get; set; } = string.Empty;
    public string Phong { get; set; } = string.Empty;


}