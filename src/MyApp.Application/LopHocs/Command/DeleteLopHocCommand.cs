using MediatR;

namespace MyApp.Application.LopHocs.Command
{
    public class DeleteLopHocCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}