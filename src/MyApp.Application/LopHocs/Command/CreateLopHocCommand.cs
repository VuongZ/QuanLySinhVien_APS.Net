using MediatR;
using MyApp.Domain.Entities;

namespace MyApp.Application.LopHocs.Command
{
    public class CreateLopHocCommand : IRequest<LopHoc>
    {
        public string TenLop { get; set; } = string.Empty;
        public string Sophong { get; set; }= string.Empty; 
        public List<CreateSinhVienInLopCommand> DanhSachSinhVien {get;set;} = new ();
    }

    public class CreateSinhVienInLopCommand
    {
          public string TenSinhVien { get; set; } = string.Empty;
        public string MaSoSinhVien { get; set; } = string.Empty;
    }
}