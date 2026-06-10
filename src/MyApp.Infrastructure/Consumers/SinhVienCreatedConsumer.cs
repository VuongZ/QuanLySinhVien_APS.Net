using System.Net.Http.Headers;
using MassTransit;
using MyApp.Domain.Documents;
using MyApp.Domain.Events;
using MyApp.Domain.Repositories;

namespace MyApp.Infrastructure.Consumers;
public class SinhVienCreatedConsumer : IConsumer<SinhVienCreatedEvent>
{
    private readonly ISinhVienMongoRepository _sinhvienMongorepo;
    private readonly ILopHocMongoRepository _lopHocMongoRepository;
     public SinhVienCreatedConsumer(ISinhVienMongoRepository sinhvienMongorepo, ILopHocMongoRepository lopHocMongoRepository)
    {
        _sinhvienMongorepo=sinhvienMongorepo;
        _lopHocMongoRepository=lopHocMongoRepository;
    }
  
    public async Task Consume(ConsumeContext<SinhVienCreatedEvent> context)
    {
        var message=context.Message;
               var sinhVienDocument = new SinhVienDocument
    {
        IdSinhVien = message.IdSinhVien,
        TenSinhVien = message.TenSinhVien,
        MaSoSinhVien = message.MaSoSinhVien,
        DanhSachLop = message.DanhSachLopHoc.Select(lh=> new LopHocDocument

        {
            IdLopHoc=lh.IdLopHoc,
            TenLop=lh.TenLop,
            Phong=lh.Phong,
            DanhSachSinhVien= new List<SinhVienDocument>()
        }).ToList()
    };
    await _sinhvienMongorepo.AddAsync(sinhVienDocument);
         foreach (var lh in message.DanhSachLopHoc)
    {
        var existing = await _lopHocMongoRepository.GetByIdAsync(lh.IdLopHoc);
        if (existing != null)
        {
            existing.DanhSachSinhVien.Add(new SinhVienDocument
            {
                IdSinhVien=sinhVienDocument.IdSinhVien,
                TenSinhVien=sinhVienDocument.TenSinhVien,
                MaSoSinhVien=sinhVienDocument.MaSoSinhVien,
                DanhSachLop= new List<LopHocDocument>()
            });
            await _lopHocMongoRepository.UpdateAsync(lh.IdLopHoc, existing);
        }
        else
        {
            var lophocDocument = new LopHocDocument
            {
                 IdLopHoc=lh.IdLopHoc,
                TenLop=lh.TenLop,
                 Phong=lh.Phong,
                DanhSachSinhVien = new List<SinhVienDocument>
                {
                    new SinhVienDocument
                    {
                        IdSinhVien = sinhVienDocument.IdSinhVien,
                        TenSinhVien = sinhVienDocument.TenSinhVien,
                        MaSoSinhVien = sinhVienDocument.MaSoSinhVien,
                        DanhSachLop = new List<LopHocDocument>() 
                    }
                }
            };
            await _lopHocMongoRepository.AddAsync(lophocDocument);
        }
    }
    }
}