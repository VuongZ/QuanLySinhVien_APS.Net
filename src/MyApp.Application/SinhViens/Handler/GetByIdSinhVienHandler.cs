
using MediatR;
using MyApp.Application.SinhViens.Query;
using MyApp.Domain.Documents;
using MyApp.Domain.Entities;
using MyApp.Domain.Repositories;

namespace MyApp.Application.SinhViens.Handler
{
    public class GetByIdSinhVienHandler : IRequestHandler<GetByIdSinhVienQuery, SinhVienDocument?>
    {
        private readonly ISinhVienMongoRepository _sinhVienMongoRepository;

        public GetByIdSinhVienHandler(ISinhVienMongoRepository sinhVienMongoRepository)
        {
            _sinhVienMongoRepository = sinhVienMongoRepository;
        }

        public async Task<SinhVienDocument?> Handle(GetByIdSinhVienQuery request, CancellationToken cancellationToken)
        {
            return await _sinhVienMongoRepository.GetByIdAsync(request.Id);
        }
    }
}
