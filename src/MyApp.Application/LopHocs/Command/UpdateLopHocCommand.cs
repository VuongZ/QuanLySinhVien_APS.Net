using MediatR;

namespace MyApp.Application.LopHocs.Command
{
    public class UpdateLopHocCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string TenLop { get; set; } = string.Empty;
        public string Sophong { get; set; }= string.Empty; 
    }
}