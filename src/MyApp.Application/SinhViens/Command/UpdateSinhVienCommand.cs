using MediatR;

namespace MyApp.Application.SinhViens.Command
{
    public class UpdateSinhVienCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string TenSinhVien { get; set; } = string.Empty;
        public string MaSoSinhVien { get; set; }= string.Empty;
    }
  
}