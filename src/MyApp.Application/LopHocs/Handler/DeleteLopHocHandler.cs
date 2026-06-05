

using MassTransit;
using MediatR;
using MyApp.Application.LopHocs.Command;
using MyApp.Domain.Events;
using MyApp.Domain.Repositories;

namespace MyApp.Application.LopHocs.Handler
{
    public class DeleteLopHocHandler : IRequestHandler<DeleteLopHocCommand, bool>
    {
       private readonly ILopHocRepository _lopHocRepository;
       private readonly IPublishEndpoint _publishEndpoint;

        public DeleteLopHocHandler(ILopHocRepository lopHocRepository, IPublishEndpoint publishEndpoint)
        {
            _lopHocRepository = lopHocRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<bool> Handle(DeleteLopHocCommand request, CancellationToken cancellationToken)
        {
            var lopHoc = await _lopHocRepository.GetByIdAsync(request.Id);
            if (lopHoc == null) return false;

            await _lopHocRepository.DeleteAsync(request.Id);
            await _publishEndpoint.Publish(new LopHocDeletedEvent
            {
                Id_LopHoc = request.Id
            }, cancellationToken);
            return true;
        }
    }
}