using MediatR;
using MyApp.Application.LopHocs.Command;
using MyApp.Domain.Entities;
using MyApp.Domain.Repositories;
using MyApp.Domain.Documents;
using MassTransit;
using MyApp.Domain.Events;
using Polly;
using Polly.Registry;

namespace MyApp.Application.LopHocs.Handler
{
    public class CreateLopHocHandler : IRequestHandler<CreateLopHocCommand, LopHoc>
    {
        private readonly ILopHocRepository _lopHocRepository;
        private readonly IPublishEndpoint _publishendpoint;
        private readonly ISinhVienRepository _SinhVienRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResiliencePipeline _retryPipeline; // thêm field

        public CreateLopHocHandler(
            ILopHocRepository lopHocRepository,
            IUnitOfWork unitOfWork,
            IPublishEndpoint publishendpoint,
            ISinhVienRepository SinhVienRepository,
            ResiliencePipelineProvider<string> pipelineProvider) // thêm tham số
        {
            _lopHocRepository = lopHocRepository;
            _publishendpoint = publishendpoint;
            _SinhVienRepository = SinhVienRepository;
            _unitOfWork = unitOfWork;
            _retryPipeline = pipelineProvider.GetPipeline("publish-retry"); // lấy pipeline đã đăng ký
        }

        public async Task<LopHoc> Handle(CreateLopHocCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            LopHoc lopHoc;
            List<SinhVien> danhsachsinhvien;
            try
            {
                lopHoc = new LopHoc
                {
                    TenLop = request.TenLop,
                    Phong = request.Sophong,
                };
                await _lopHocRepository.AddAsync(lopHoc);

                danhsachsinhvien = new List<SinhVien>();
                foreach (var sv in request.DanhSachSinhVien)
                {
                    var sinhvien = new SinhVien
                    {
                        TenSinhVien = sv.TenSinhVien,
                        MaSoSinhVien = sv.MaSoSinhVien
                    };
                    await _SinhVienRepository.AddAsync(sinhvien);
                    danhsachsinhvien.Add(sinhvien);
                }

                await _unitOfWork.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync(cancellationToken);
                throw;
            }

            var lopHocEvent = new LopHocCreatedEvent
            {
                IdLopHoc = lopHoc.Id,
                TenLop = lopHoc.TenLop,
                Phong = lopHoc.Phong,
                DanhSachSinhVien = danhsachsinhvien.Select(sv => new SinhVienInLopEvent
                {
                    IdSinhVien = sv.Id,
                    TenSinhVien = sv.TenSinhVien,
                    MaSoSinhVien = sv.MaSoSinhVien
                }).ToList()
            };

            await _retryPipeline.ExecuteAsync(async ct =>
            {
                await _publishendpoint.Publish(lopHocEvent, ct);
            }, cancellationToken);

            return lopHoc;
        }
    }
}