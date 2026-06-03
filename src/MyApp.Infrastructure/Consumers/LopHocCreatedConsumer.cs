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
        Id_LopHoc = message.Id_LopHoc,
        TenLop = message.TenLop,
        Phong = message.Phong,
        DanhSachSinhVien = new List<SinhVienDocument>()
    };
        var danhsachSinhVien=message.DanhSachSinhVien.Select(sv=> new SinhVienDocument
        {
                Id_SinhVien = sv.Id_SinhVien,
            TenSinhVien = sv.TenSinhVien,
            MaSoSinhVien = sv.MaSoSinhVien,
            DanhSachLop = new List<LopHocDocument>{lopHocDocument}
        }).ToList();
        var document = new LopHocDocument
        {
            Id_LopHoc = message.Id_LopHoc,
            TenLop = message.TenLop,
            Phong = message.Phong,
            DanhSachSinhVien = danhsachSinhVien
        };

        await _lopHocMongoRepository.AddAsync(document);
         foreach (var sv in danhsachSinhVien)
        {
            await _SinhVienMongoRepository.AddAsync(sv);
        }
    }
}