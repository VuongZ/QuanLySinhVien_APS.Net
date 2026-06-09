namespace MyApp.Infrastructure.Consumers;
using MassTransit;
using MyApp.Domain.Events;
using MyApp.Domain.Repositories;

public class SinhVienDeletedConsumer : IConsumer<SinhVienDeletedEvent>
{
    private readonly ISinhVienMongoRepository _sinhVienMongoRepository;
    public SinhVienDeletedConsumer(ISinhVienMongoRepository sinhVienMongoRepository)
    {
        _sinhVienMongoRepository = sinhVienMongoRepository;
    }
    public async Task Consume(ConsumeContext<SinhVienDeletedEvent> context)
    {
       await _sinhVienMongoRepository.DeleteAsync(context.Message.IdSinhVien);
    }
}