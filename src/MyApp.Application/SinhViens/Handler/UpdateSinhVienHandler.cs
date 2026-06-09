
using MassTransit;
using MediatR;
using MyApp.Application.SinhViens.Command;
using MyApp.Domain.Entities;
using MyApp.Domain.Events;
using MyApp.Domain.Repositories;

namespace MyApp.Application.SinhViens.Handler
{
   public class UpdateSinhVienHandler : IRequestHandler<UpdateSinhVienCommand, bool>
    {
        private readonly ISinhVienRepository _sinhVienRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSinhVienHandler(ISinhVienRepository sinhVienRepository, IPublishEndpoint publishEndpoint, IUnitOfWork unitOfWork)
        {
            _sinhVienRepository = sinhVienRepository;
            _publishEndpoint = publishEndpoint;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(UpdateSinhVienCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
            var sinhVien = await _sinhVienRepository.GetByIdAsync(request.Id);
            if (sinhVien == null)
            {
                 await _unitOfWork.RollbackAsync(cancellationToken);
                return false;
            }
            sinhVien.TenSinhVien = request.TenSinhVien;
            sinhVien.MaSoSinhVien = request.MaSoSinhVien;

            await _sinhVienRepository.UpdateAsync(sinhVien);
            await _unitOfWork.CommitAsync(cancellationToken);
            await _publishEndpoint.Publish(new SinhVienUpdatedEvent
            {
                IdSinhVien = request.Id,
                TenSinhVien = request.TenSinhVien,
                MaSoSinhVien = request.MaSoSinhVien
            }, cancellationToken);
            return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}