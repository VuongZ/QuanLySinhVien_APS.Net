using MediatR;
using MyApp.Domain.Documents;
using MyApp.Domain.Entities;

namespace MyApp.Application.LopHocs.Query
{
    public class GetByIDLopHocQuery : IRequest<LopHocDocument?>
    {
        public int Id { get; set; }
    }
}