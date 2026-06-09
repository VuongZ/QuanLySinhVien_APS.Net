namespace MyApp.Domain.Entities;

public class LopHoc : BaseId<int>
{
    public string TenLop { get; set; } = string.Empty;
    public string Phong { get; set; } = string.Empty;

 
}