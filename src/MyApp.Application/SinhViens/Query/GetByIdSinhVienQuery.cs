
using MediatR;
using MyApp.Domain.Documents;
using MyApp.Domain.Entities;

namespace MyApp.Application.SinhViens.Query
{
   public class GetByIdSinhVienQuery : IRequest<SinhVienDocument>
    {
        public int Id  { get; set; }  
    }
}