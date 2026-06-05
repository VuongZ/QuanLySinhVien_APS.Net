using Grpc.Core;
using MediatR;
using MyApp.Application.LopHocs.Command;
using MyApp.Application.LopHocs.Query;
using MyApp.Contracts.Protos;

namespace MyApp.Presentation.Services;

public class LopHocGrpcService : LopHocGrpc.LopHocGrpcBase
{
    private readonly IMediator _mediator;

    public LopHocGrpcService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<GetAllLopHocsResponse> GetAllLopHocs(
        GetAllLopHocsRequest request, ServerCallContext context)
    {
        var query = new GetAllLopHocQuery();
        var lopHocs = await _mediator.Send(query);
        var response = new GetAllLopHocsResponse();
        response.LopHocs.AddRange(lopHocs.Select(l => new LopHoc
        {
            IdLopHoc = l.Id_LopHoc,
            Tenlop = l.TenLop,
            Phong = l.Phong
        }));
        return response;
    }

    public override async Task<LopHocResponse> GetLopHocById(
        GetLopHocByIdRequest request, ServerCallContext context)
    {
        var query = new GetByIDLopHocQuery { Id = request.Id };
        var lopHoc = await _mediator.Send(query);
        if (lopHoc == null)
            throw new RpcException(new Status(
                StatusCode.NotFound, $"Lop hoc voi ID {request.Id} khong ton tai"));

        return new LopHocResponse
        {
            IdLopHoc = lopHoc.Id_LopHoc,
            Tenlop = lopHoc.TenLop,
            Phong = lopHoc.Phong
        };
    }

    public override async Task<CreateLopHocResponse> CreateLopHoc(
        CreateLopHocRequest request, ServerCallContext context)
    {
        var command = new CreateLopHocCommand
        {
            TenLop = request.Tenlop,
            Sophong = request.Phong,
            DanhSachSinhVien = request.Danhsachsinhvien.Select(sv =>
            new CreateSinhVienInLopCommand
            {
                TenSinhVien = sv.Tensv,
                MaSoSinhVien = sv.Mssv
            }
            ).ToList()
        };
        var lopHoc = await _mediator.Send(command);
        return new CreateLopHocResponse { IdLopHoc   = lopHoc.Id_LopHoc };
    }

    public override async Task<UpdateLopHocResponse> UpdateLopHoc(
        UpdateLopHocRequest request, ServerCallContext context)
    {
        var command = new UpdateLopHocCommand
        {
            Id = request.IdLopHoc,
            TenLop = request.Tenlop,
            Sophong = request.Phong
        };
        await _mediator.Send(command);
        return new UpdateLopHocResponse { Success = true };
    }

    public override async Task<DeleteLopHocResponse> DeleteLopHoc(
        DeleteLopHocRequest request, ServerCallContext context)
    {
        var command = new DeleteLopHocCommand { Id = request.IdLopHoc };
        await _mediator.Send(command);
        return new DeleteLopHocResponse { Success = true };
    }
}