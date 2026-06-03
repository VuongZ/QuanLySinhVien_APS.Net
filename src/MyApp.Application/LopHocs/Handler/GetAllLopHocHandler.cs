
using MediatR;
using MyApp.Application.LopHocs.Query;
using MyApp.Domain.Documents;
using MyApp.Domain.Entities;
using MyApp.Domain.Repositories;

namespace MyApp.Application.LopHocs.Handler
{
    public class GetAllLopHocHandler : IRequestHandler<GetAllLopHocQuery, IEnumerable<LopHocDocument>>
    {
        private readonly ILopHocMongoRepository _lopHocMonGoRepository;
        public GetAllLopHocHandler(ILopHocMongoRepository lopHocMonGoRepository)
        {
            _lopHocMonGoRepository = lopHocMonGoRepository;
        }
        public async Task<IEnumerable<LopHocDocument>> Handle(GetAllLopHocQuery request, CancellationToken cancellationToken)
        {
            return await _lopHocMonGoRepository.GetAllAsync();
        }
        
    }
}