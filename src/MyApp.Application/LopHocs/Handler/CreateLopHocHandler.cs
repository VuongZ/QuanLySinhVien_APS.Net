
using MediatR;
using MyApp.Application.LopHocs.Command;
using MyApp.Domain.Entities;
using MyApp.Domain.Repositories;
using MyApp.Domain.Documents;
using MassTransit;
using MyApp.Domain.Events;
namespace MyApp.Application.LopHocs.Handler
{
    public class CreateLopHocHandler : IRequestHandler<CreateLopHocCommand, LopHoc>

    {
        private readonly ILopHocRepository _lopHocRepository;
        private readonly IPublishEndpoint _publishendpoint;
        private readonly ISinhVienRepository _SinhVienRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateLopHocHandler(ILopHocRepository lopHocRepository,  IUnitOfWork unitOfWork,IPublishEndpoint publishendpoint, ISinhVienRepository SinhVienRepository)
        {
            _lopHocRepository = lopHocRepository;
            _publishendpoint=publishendpoint;
            _SinhVienRepository=SinhVienRepository;
            _unitOfWork=unitOfWork;
        }
        public async Task<LopHoc> Handle(CreateLopHocCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
            var lopHoc = new LopHoc
            {
                TenLop  =  request.TenLop,
                Phong = request.Sophong,
            };
            await _lopHocRepository.AddAsync(lopHoc);

            var danhsachsinhvien=new List<SinhVien>();
           foreach (var sv in request.DanhSachSinhVien) 
            {
                var sinhvien=new SinhVien
                {
                TenSinhVien=sv.TenSinhVien,
                MaSoSinhVien=sv.MaSoSinhVien
                };
                await _SinhVienRepository.AddAsync(sinhvien);
                danhsachsinhvien.Add(sinhvien);
            };
            await _unitOfWork.CommitAsync(cancellationToken);
             await _publishendpoint.Publish(new LopHocCreatedEvent
            {
                Id_LopHoc=lopHoc.Id_LopHoc,
                TenLop=lopHoc.TenLop,
                Phong=lopHoc.Phong,
                DanhSachSinhVien = danhsachsinhvien.Select(sv=>new SinhVienInLopEvent
                  { Id_SinhVien = sv.Id_SinhVien,
                TenSinhVien = sv.TenSinhVien,
                MaSoSinhVien = sv.MaSoSinhVien
                }).ToList()
                 },cancellationToken);
                       return lopHoc;
        }
        catch(Exception)
            {
                await _unitOfWork.RollbackAsync(cancellationToken);
                throw;
            }
        }
       
    }
}