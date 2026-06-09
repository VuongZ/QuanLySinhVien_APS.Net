using MassTransit;
using MyApp.Domain.Documents;
using MyApp.Domain.Events;
using MyApp.Domain.Repositories;

namespace MyApp.Infrastructure.Consumers;

public class LopHocCreatedConsumer : IConsumer<LopHocCreatedEvent>
{
    private readonly ILopHocMongoRepository _lopHocMongoRepository;
    private readonly ISinhVienMongoRepository _SinhVienMongoRepository;

    public LopHocCreatedConsumer(ILopHocMongoRepository lopHocMongoRepository, ISinhVienMongoRepository sinhVienMongoRepository)
    {
        _lopHocMongoRepository = lopHocMongoRepository;
        _SinhVienMongoRepository = sinhVienMongoRepository;
    }

    public async Task Consume(ConsumeContext<LopHocCreatedEvent> context)
    {
        var message = context.Message;
          var lopHocDocument = new LopHocDocument
    {
        IdLopHoc = message.IdLopHoc,
        TenLop = message.TenLop,
        Phong = message.Phong,
        DanhSachSinhVien = message.DanhSachSinhVien.Select(sv=> new SinhVienDocument
       {
                IdSinhVien = sv.IdSinhVien,
            TenSinhVien = sv.TenSinhVien,
            MaSoSinhVien = sv.MaSoSinhVien,
            DanhSachLop = new List<LopHocDocument>()
        }).ToList()
    };
     await _lopHocMongoRepository.AddAsync(lopHocDocument);
         foreach (var sv in message.DanhSachSinhVien)
    {
        var existing = await _SinhVienMongoRepository.GetByIdAsync(sv.IdSinhVien);
        if (existing != null)
        {
            existing.DanhSachLop.Add(new LopHocDocument
            {
                IdLopHoc = lopHocDocument.IdLopHoc,
                TenLop = lopHocDocument.TenLop,
                Phong = lopHocDocument.Phong,
                DanhSachSinhVien = new List<SinhVienDocument>()
            });
            await _SinhVienMongoRepository.UpdateAsync(sv.IdSinhVien, existing);
        }
        else
        {
            var sinhVienDocument = new SinhVienDocument
            {
                IdSinhVien = sv.IdSinhVien,
                TenSinhVien = sv.TenSinhVien,
                MaSoSinhVien = sv.MaSoSinhVien,
                DanhSachLop = new List<LopHocDocument>
                {
                    new LopHocDocument
                    {
                        IdLopHoc = lopHocDocument.IdLopHoc,
                        TenLop = lopHocDocument.TenLop,
                        Phong = lopHocDocument.Phong,
                        DanhSachSinhVien = new List<SinhVienDocument>() 
                    }
                }
            };
            await _SinhVienMongoRepository.AddAsync(sinhVienDocument);
        }
    }
    }
}