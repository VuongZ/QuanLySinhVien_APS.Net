using MediatR;
using MyApp.Domain.Documents;
using MyApp.Domain.Entities;

namespace MyApp.Application.LopHocs.Query
{
    public class GetAllLopHocQuery : IRequest<IEnumerable<LopHocDocument>>
    {
    }
}