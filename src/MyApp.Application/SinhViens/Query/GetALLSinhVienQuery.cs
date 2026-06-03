
using MediatR;
using MyApp.Domain.Documents;
using MyApp.Domain.Entities;

namespace MyApp.Application.SinhViens.Query

{
  public class GetALLSinhVienQuery : IRequest<IEnumerable<SinhVienDocument>>
    {
       
    }
}