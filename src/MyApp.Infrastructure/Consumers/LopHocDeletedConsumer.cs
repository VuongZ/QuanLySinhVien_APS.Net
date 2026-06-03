namespace MyApp.Infrastructure.Consumers;
using MassTransit;
using MyApp.Domain.Events;
using MyApp.Domain.Repositories;

public class LopHocDeletedConsumer : IConsumer<LopHocDeletedEvent>
{
    private readonly ILopHocMongoRepository _lopHocMongoRepository;
    public LopHocDeletedConsumer(ILopHocMongoRepository lopHocMongoRepository)
    {
        _lopHocMongoRepository = lopHocMongoRepository;
    }
    public async Task Consume(ConsumeContext<LopHocDeletedEvent> context)
    {
       await _lopHocMongoRepository.DeleteAsync(context.Message.Id_LopHoc);
    }
}