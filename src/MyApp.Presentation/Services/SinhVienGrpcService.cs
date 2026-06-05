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
            IdSinhVien = s.Id_SinhVien,
            Tensv = s.TenSinhVien,
            Mssv = s.MaSoSinhVien
        }));
        return response;
    }

    public override async Task<SinhVienResponse> GetSinhVienById(
        GetSinhVienByIdRequest request, ServerCallContext context)
    {
        var query = new GetByIdSinhVienQuery { Id = request.IdSinhVien };
        var sinhVien = await _mediator.Send(query);
        if (sinhVien == null)
            throw new RpcException(new Status(
                StatusCode.NotFound, $"Sinh vien voi ID {request.IdSinhVien} khong ton tai"));

        return new SinhVienResponse
        {
            IdSinhVien = sinhVien.Id_SinhVien,
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
        return new CreateSinhVienResponse { IdSinhVien = sinhVien.Id_SinhVien };
    }

    public override async Task<UpdateSinhVienResponse> UpdateSinhVien(
        UpdateSinhVienRequest request, ServerCallContext context)
    {
        var command = new UpdateSinhVienCommand
        {
            Id_SinhVien = request.IdSinhVien,
            TenSinhVien = request.Tensv,
            MaSoSinhVien = request.Mssv
        };
        await _mediator.Send(command);
        return new UpdateSinhVienResponse { Success = true };
    }

    public override async Task<DeleteSinhVienResponse> DeleteSinhVien(
        DeleteSinhVienRequest request, ServerCallContext context)
    {
        var command = new DeleteSinhVienCommand { Id_SinhVien = request.IdSinhVien };
        await _mediator.Send(command);
        return new DeleteSinhVienResponse { Success = true };
    }
}