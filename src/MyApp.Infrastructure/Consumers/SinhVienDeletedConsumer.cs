namespace MyApp.Infrastructure.Consumers;
using MassTransit;
using MyApp.Domain.Events;
using MyApp.Domain.Repositories;

public class SinhVienDeletedConsumer : IConsumer<SinhVienDeletedEvent>
{
    private readonly ILopHocMongoRepository _lopHocMongoRepository;
    private readonly ISinhVienMongoRepository _sinhVienMongoRepository;
    public SinhVienDeletedConsumer(ILopHocMongoRepository lopHocMongoRepository,ISinhVienMongoRepository sinhVienMongoRepository)
    {
        _lopHocMongoRepository = lopHocMongoRepository;
        _sinhVienMongoRepository=sinhVienMongoRepository;
    }
    public async Task Consume(ConsumeContext<SinhVienDeletedEvent> context)
    {
       await _sinhVienMongoRepository.DeleteAsync(context.Message.IdSinhVien);
       var allLopHoc = await _lopHocMongoRepository.GetAllAsync();
        foreach (var lh in allLopHoc)
        {
            var hassv = lh.DanhSachSinhVien.Any(l => l.IdSinhVien == context.Message.IdSinhVien );
            if (hassv)
            {
                lh.DanhSachSinhVien.RemoveAll(l => l.IdSinhVien == context.Message.IdSinhVien);
                await _lopHocMongoRepository.UpdateAsync(lh.IdLopHoc, lh);
            }
        }
    }
}