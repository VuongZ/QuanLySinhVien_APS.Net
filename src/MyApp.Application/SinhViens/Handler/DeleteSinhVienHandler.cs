
using MassTransit;
using MediatR;
using MyApp.Application.SinhViens.Command;
using MyApp.Domain.Events;
using MyApp.Domain.Repositories;

namespace MyApp.Application.SinhViens.Handler
{
    public class DeleteSinhVienHandler : IRequestHandler<DeleteSinhVienCommand, bool>
    {
        private readonly ISinhVienRepository _sinhVienRepository;
                private readonly IPublishEndpoint _publishEndpoint;


        public DeleteSinhVienHandler(ISinhVienRepository sinhVienRepository, IPublishEndpoint publishEndpoint)
        {
            _sinhVienRepository = sinhVienRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<bool> Handle(DeleteSinhVienCommand request, CancellationToken cancellationToken)
        {
            var sv= await _sinhVienRepository.GetByIdAsync(request.Id_SinhVien);
            await _sinhVienRepository.DeleteAsync(request.Id_SinhVien);
            await _publishEndpoint.Publish(new SinhVienDeletedEvent
            {
                Id_SinhVien = request.Id_SinhVien
            }, cancellationToken);
            return true;
        }
        
    }
}