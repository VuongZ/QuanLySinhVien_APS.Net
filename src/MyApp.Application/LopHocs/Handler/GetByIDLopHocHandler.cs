
using MediatR;
using MyApp.Application.LopHocs.Query;
using MyApp.Domain.Documents;
using MyApp.Domain.Entities;
using MyApp.Domain.Repositories;

namespace MyApp.Application.LopHocs.Handler
{
    public class GetByIDLopHocHandler : IRequestHandler<GetByIDLopHocQuery, LopHocDocument?>
    {
        private readonly ILopHocMongoRepository _lopHocMonGoRepository;
        public GetByIDLopHocHandler(ILopHocMongoRepository lopHocMonGoRepository)
        {
            _lopHocMonGoRepository = lopHocMonGoRepository;
        }
        public async Task<LopHocDocument?> Handle(GetByIDLopHocQuery request, CancellationToken cancellationToken)
        {
            return await _lopHocMonGoRepository.GetByIdAsync(request.Id);
        }
        
    }
}