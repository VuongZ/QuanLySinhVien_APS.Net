using MassTransit;
using MediatR;
using MyApp.Application.SinhViens.Command;
using MyApp.Domain.Documents;
using MyApp.Domain.Entities;
using MyApp.Domain.Events;
using MyApp.Domain.Repositories;
using Polly;
using Polly.Registry;

namespace MyApp.Application.SinhViens.Handler
{
    public class CreateSinhVienHandler : IRequestHandler<CreateSinhVienCommand, SinhVien>
    {
        private readonly ISinhVienRepository _sinhVienRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILopHocRepository _lophocrepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResiliencePipeline _retryPipeline; // thêm field

        public CreateSinhVienHandler(
            ISinhVienRepository sinhVienRepository,
            IUnitOfWork unitOfWork,
            IPublishEndpoint publishEndpoint,
            ILopHocRepository lophocrepository,
            ResiliencePipelineProvider<string> pipelineProvider) // thêm tham số
        {
            _sinhVienRepository = sinhVienRepository;
            _publishEndpoint = publishEndpoint;
            _lophocrepository = lophocrepository;
            _unitOfWork = unitOfWork;
            _retryPipeline = pipelineProvider.GetPipeline("publish-retry"); // lấy pipeline đã đăng ký
        }

        public async Task<SinhVien> Handle(CreateSinhVienCommand request, CancellationToken cancellationToken)
        {
            // Vùng 1: DB transaction
            SinhVien sinhVien;
            List<LopHoc> danhsachlophoc;

            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                sinhVien = new SinhVien
                {
                    TenSinhVien = request.TenSinhVien,
                    MaSoSinhVien = request.MaSoSinhVien,
                };
                await _sinhVienRepository.AddAsync(sinhVien);

                danhsachlophoc = new List<LopHoc>();
                foreach (var l in request.DanhSachLopHoc)
                {
                    var lopHoc = new LopHoc
                    {
                        TenLop = l.TenLop,
                        Phong = l.Sophong,
                    };
                    await _lophocrepository.AddAsync(lopHoc);
                    danhsachlophoc.Add(lopHoc);
                }

                await _unitOfWork.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync(cancellationToken);
                throw;
            }

            // Vùng 2: Publish với retry (tách riêng khỏi transaction)
            var sinhVienEvent = new SinhVienCreatedEvent
            {
                IdSinhVien = sinhVien.Id,
                TenSinhVien = sinhVien.TenSinhVien,
                MaSoSinhVien = sinhVien.MaSoSinhVien,
                DanhSachLopHoc = danhsachlophoc.Select(lh => new LopHocInEvent
                {
                    IdLopHoc = lh.Id,
                    TenLop = lh.TenLop,
                    Phong = lh.Phong
                }).ToList()
            };

            await _retryPipeline.ExecuteAsync(async ct =>
            {
                await _publishEndpoint.Publish(sinhVienEvent, ct);
            }, cancellationToken);

            return sinhVien;
        }
    }
}