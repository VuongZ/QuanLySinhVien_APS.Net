using MediatR;

namespace MyApp.Application.SinhViens.Command
{
    public class DeleteSinhVienCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}