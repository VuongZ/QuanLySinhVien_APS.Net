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
        DanhSachLop = new List<LopHocDocument>()
    };
        var danhsachlophoc=message.DanhSachLopHoc.Select(lh=> new LopHocDocument
        {
            IdLopHoc=lh.IdLopHoc,
            TenLop=lh.TenLop,
            Phong=lh.Phong,
            DanhSachSinhVien= new List<SinhVienDocument>{ sinhVienDocument }
        }).ToList();
        var document= new SinhVienDocument
        {
            IdSinhVien=message.IdSinhVien,
            TenSinhVien=message.TenSinhVien,
            MaSoSinhVien=message.MaSoSinhVien,
            DanhSachLop=danhsachlophoc
            
        };
        await _sinhvienMongorepo.AddAsync(document);
        foreach(var lh in danhsachlophoc)
        {
           
                await _lopHocMongoRepository.AddAsync(lh);

        }
    }
}