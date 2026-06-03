namespace MyApp.Infrastructure.Consumers;

using global::MyApp.Domain.Events;
using global::MyApp.Domain.Repositories;
using MassTransit;
public class SinhVienUpdatedConsumer : IConsumer<SinhVienUpdatedEvent>
{
    private readonly ISinhVienMongoRepository _sinhVienMongoRepository;
    public SinhVienUpdatedConsumer(ISinhVienMongoRepository sinhVienMongoRepository)
    {
        _sinhVienMongoRepository = sinhVienMongoRepository;
    }
    public async Task Consume(ConsumeContext<SinhVienUpdatedEvent> context)
    {
       var sinhVien = await _sinhVienMongoRepository.GetByIdAsync(context.Message.Id_SinhVien);
       if(sinhVien != null)
       {
            sinhVien.TenSinhVien = context.Message.TenSinhVien;
            sinhVien.MaSoSinhVien = context.Message.MaSoSinhVien;
            await _sinhVienMongoRepository.UpdateAsync(context.Message.Id_SinhVien, sinhVien);
       }
    }
}