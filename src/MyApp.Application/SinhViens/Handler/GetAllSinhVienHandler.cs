
using MediatR;
using MyApp.Application.SinhViens.Query;
using MyApp.Domain.Documents;
using MyApp.Domain.Entities;
using MyApp.Domain.Repositories;

namespace MyApp.Application.SinhViens.Handler
{
    public class GetAllSinhVienHandler : IRequestHandler<GetALLSinhVienQuery, IEnumerable<SinhVienDocument>>
    {
        private readonly ISinhVienMongoRepository _sinhVienMongoRepository;

        public GetAllSinhVienHandler(ISinhVienMongoRepository sinhVienMongoRepository)
        {
            _sinhVienMongoRepository = sinhVienMongoRepository;
        }

        public async Task<IEnumerable<SinhVienDocument>> Handle(GetALLSinhVienQuery request, CancellationToken cancellationToken)
        {
            return await _sinhVienMongoRepository.GetAllAsync();
        }
    }
}