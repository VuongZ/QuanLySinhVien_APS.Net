namespace MyApp.Infrastructure.Consumers;
using MassTransit;
using MyApp.Domain.Events;
using MyApp.Domain.Repositories;

public class LopHocDeletedConsumer : IConsumer<LopHocDeletedEvent>
{
    private readonly ILopHocMongoRepository _lopHocMongoRepository;
    private readonly ISinhVienMongoRepository _sinhVienMongoRepository;
    public LopHocDeletedConsumer(ILopHocMongoRepository lopHocMongoRepository,ISinhVienMongoRepository sinhVienMongoRepository)
    {
        _lopHocMongoRepository = lopHocMongoRepository;
        _sinhVienMongoRepository=sinhVienMongoRepository;
    }
    public async Task Consume(ConsumeContext<LopHocDeletedEvent> context)
    {
       await _lopHocMongoRepository.DeleteAsync(context.Message.IdLopHoc);
       var allSinhVien = await _sinhVienMongoRepository.GetAllAsync();
        foreach (var sv in allSinhVien)
        {
            var hasLop = sv.DanhSachLop.Any(l => l.IdLopHoc == context.Message.IdLopHoc );
            if (hasLop)
            {
                sv.DanhSachLop.RemoveAll(l => l.IdLopHoc == context.Message.IdLopHoc);
                await _sinhVienMongoRepository.UpdateAsync(sv.IdSinhVien, sv);
            }
        }
    }
}