
using MassTransit;
using MediatR;
using MyApp.Application.SinhViens.Command;
using MyApp.Domain.Documents;
using MyApp.Domain.Entities;
using MyApp.Domain.Events;
using MyApp.Domain.Repositories;
namespace MyApp.Application.SinhViens.Handler
{
 public class CreateSinhVienHandler : IRequestHandler<CreateSinhVienCommand, SinhVien>
    {
        private readonly ISinhVienRepository _sinhVienRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILopHocRepository _lophocrepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSinhVienHandler(ISinhVienRepository sinhVienRepository,IUnitOfWork unitOfWork,IPublishEndpoint publishEndpoin,ILopHocRepository lophocrepository)
        {
            _sinhVienRepository = sinhVienRepository;
            _publishEndpoint=publishEndpoin;
            _lophocrepository=lophocrepository;
            _unitOfWork=unitOfWork;
        }

        public async Task<SinhVien> Handle(CreateSinhVienCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try{
               var sinhVien = new SinhVien
            {
                TenSinhVien = request.TenSinhVien,
                MaSoSinhVien = request.MaSoSinhVien,
                
            };
            await _sinhVienRepository.AddAsync(sinhVien);
            var danhsachlophoc=new List<LopHoc>();
            foreach(var l in request.DanhSachLopHoc)
            {
                var lopHoc = new LopHoc
            {
                TenLop  = l.TenLop,
                Phong = l.Sophong,
            };
            await _lophocrepository.AddAsync(lopHoc);
            danhsachlophoc.Add(lopHoc);
            }
                        await _unitOfWork.CommitAsync(cancellationToken);

             await _publishEndpoint.Publish(new SinhVienCreatedEvent
            {
                Id_SinhVien=sinhVien.Id_SinhVien,
                TenSinhVien=sinhVien.TenSinhVien,
                MaSoSinhVien=sinhVien.MaSoSinhVien,
                DanhSachLopHoc = danhsachlophoc.Select(lh=>new LopHocInEvent
                  { Id_LopHoc = lh.Id_LopHoc,
                TenLop = lh.TenLop,
                Phong = lh.Phong
                }).ToList()
                 },cancellationToken);
          return sinhVien;
        }catch (Exception)
            {
                await _unitOfWork.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}