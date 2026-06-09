using MassTransit;
using MyApp.Domain.Events;
using MyApp.Domain.Repositories;

namespace MyApp.Infrastructure.Consumers;
public class LopHocUpdatedConsumer : IConsumer<LopHocUpdatedEvent>
{
    private readonly ILopHocMongoRepository _lopHocMongoRepository;
    public LopHocUpdatedConsumer(ILopHocMongoRepository lopHocMongoRepository)
    {
        _lopHocMongoRepository = lopHocMongoRepository;
    }
    public async Task Consume(ConsumeContext<LopHocUpdatedEvent> context)
    {
       var lopHoc = await _lopHocMongoRepository.GetByIdAsync(context.Message.IdLopHoc);
       if(lopHoc != null)
       {
            lopHoc.TenLop = context.Message.TenLop;
            lopHoc.Phong = context.Message.Phong;
            await _lopHocMongoRepository.UpdateAsync(context.Message.IdLopHoc, lopHoc);
       }
    }
}