using MediatR;
using MyApp.Domain.Entities;
namespace MyApp.Application.SinhViens.Command
{
    public class CreateSinhVienCommand : IRequest<SinhVien>
    {
        public string TenSinhVien { get; set; } = string.Empty;
        public string MaSoSinhVien { get; set; } = string.Empty;
        public List<CreateLopHocInCommand> DanhSachLopHoc {get; set;}=new ();
    }

    public class CreateLopHocInCommand
    {
        public string TenLop { get; set; } = string.Empty;
        public string Sophong { get; set; }= string.Empty; 
    }
}