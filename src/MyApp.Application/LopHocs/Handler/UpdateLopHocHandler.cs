
    using MassTransit;
    using MediatR;
    using MyApp.Application.LopHocs.Command;
    using MyApp.Domain.Events;
    using MyApp.Domain.Repositories;

    namespace MyApp.Application.LopHocs.Handler
    {
        public class UpdateLopHocHandler : IRequestHandler<UpdateLopHocCommand, bool> 
        {
            private readonly ILopHocRepository _lopHocRepository;
            private readonly IPublishEndpoint _publishEndpoint;
            private readonly IUnitOfWork _unitOfWork;
            public UpdateLopHocHandler(ILopHocRepository lopHocRepository, IPublishEndpoint publishEndpoint, IUnitOfWork unitOfWork)
            {
                _lopHocRepository = lopHocRepository;
                _publishEndpoint = publishEndpoint;
                _unitOfWork = unitOfWork;
            }
            public async Task<bool> Handle(UpdateLopHocCommand request, CancellationToken cancellationToken)
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);
                try
                {
                var lopHoc = await _lopHocRepository.GetByIdAsync(request.Id);
                if (lopHoc == null)
                {
                     await _unitOfWork.RollbackAsync(cancellationToken);
                    return false;
                }

                lopHoc.TenLop = request.TenLop;
                lopHoc.Phong = request.Sophong;

                await _lopHocRepository.UpdateAsync(lopHoc);
                await _unitOfWork.CommitAsync(cancellationToken);
                await _publishEndpoint.Publish(new LopHocUpdatedEvent
                {
                    Id_LopHoc = request.Id,
                    TenLop = request.TenLop,
                    Phong = request.Sophong
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