using Grpc.Core;
using MediatR;
using MyApp.Application.SinhViens.Command;
using MyApp.Application.SinhViens.Query;
using MyApp.Contracts.Protos;

namespace MyApp.Presentation.Services;

public class SinhVienGrpcService : SinhVienGrpc.SinhVienGrpcBase
{
    private readonly IMediator _mediator;

    public SinhVienGrpcService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<GetAllSinhViensResponse> GetAllSinhViens(
        GetAllSinhViensRequest request, ServerCallContext context)
    {
        var query = new GetALLSinhVienQuery();
        var sinhViens = await _mediator.Send(query);
        var response = new GetAllSinhViensResponse();
        response.SinhViens.AddRange(sinhViens.Select(s => new SinhVien
        {
            Id = s.IdSinhVien,
            Tensv = s.TenSinhVien,
            Mssv = s.MaSoSinhVien
        }));
        return response;
    }

    public override async Task<SinhVienResponse> GetSinhVienById(
        GetSinhVienByIdRequest request, ServerCallContext context)
    {
        var query = new GetByIdSinhVienQuery { Id = request.Id };
        var sinhVien = await _mediator.Send(query);
        if (sinhVien == null)
            throw new RpcException(new Status(
                StatusCode.NotFound, $"Sinh vien voi ID {request.Id} khong ton tai"));

        return new SinhVienResponse
        {
            Id = sinhVien.IdSinhVien,
            Tensv = sinhVien.TenSinhVien,
            Mssv = sinhVien.MaSoSinhVien
        };
    }

    public override async Task<CreateSinhVienResponse> CreateSinhVien(
        CreateSinhVienRequest request, ServerCallContext context)
    {
        var command = new CreateSinhVienCommand
        {
            TenSinhVien = request.Tensv,
            MaSoSinhVien = request.Mssv,
            DanhSachLopHoc=request.Danhsachlophoc.Select(lh=>new  CreateLopHocInCommand
            {
                TenLop=lh.Tenlop,
                Sophong=lh.Phong

            }).ToList()
        };
        var sinhVien = await _mediator.Send(command);
        return new CreateSinhVienResponse { Id = sinhVien.Id };
    }

    public override async Task<UpdateSinhVienResponse> UpdateSinhVien(
        UpdateSinhVienRequest request, ServerCallContext context)
    {
        var command = new UpdateSinhVienCommand
        {
            Id = request.Id,
            TenSinhVien = request.Tensv,
            MaSoSinhVien = request.Mssv
        };
        await _mediator.Send(command);
        return new UpdateSinhVienResponse { Success = true };
    }

    public override async Task<DeleteSinhVienResponse> DeleteSinhVien(
        DeleteSinhVienRequest request, ServerCallContext context)
    {
        var command = new DeleteSinhVienCommand { Id = request.Id };
        await _mediator.Send(command);
        return new DeleteSinhVienResponse { Success = true };
    }
}