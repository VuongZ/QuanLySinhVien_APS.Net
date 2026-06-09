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
        response.LopHocs.AddRange(lopHocs.Select(l =>
        {
            var lopHoc=new LopHoc
            {
            Id = l.IdLopHoc,
            Tenlop = l.TenLop,
            Phong = l.Phong

        };
        lopHoc.Danhsachsinhvien.AddRange(l.DanhSachSinhVien.Select(sv => new SinhVienInfo
        {
            Id=sv.IdSinhVien,
            Tensv=sv.TenSinhVien,
            Mssv=sv.MaSoSinhVien
        }
        ));
        return lopHoc;
    }));
        return response;
    }

    public override async Task<LopHocResponse> GetLopHocById(
        GetLopHocByIdRequest request, ServerCallContext context)
    {
    var lopHoc = await _mediator.Send(new GetByIDLopHocQuery { Id = request.Id });
    if (lopHoc == null)
        throw new RpcException(new Status(StatusCode.NotFound, $"Lop hoc {request.Id} khong ton tai"));

    var response = new LopHocResponse
    {
        Id = lopHoc.IdLopHoc,
        Tenlop = lopHoc.TenLop,
        Phong = lopHoc.Phong
    };
    response.Danhsachsinhvien.AddRange(lopHoc.DanhSachSinhVien.Select(sv => new SinhVienInfo
    {
        Id = sv.IdSinhVien,
        Tensv = sv.TenSinhVien,
        Mssv = sv.MaSoSinhVien
    }));
    return response;
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
        return new CreateLopHocResponse { Id   = lopHoc.Id };
    }

    public override async Task<UpdateLopHocResponse> UpdateLopHoc(
        UpdateLopHocRequest request, ServerCallContext context)
    {
        var command = new UpdateLopHocCommand
        {
            Id = request.Id,
            TenLop = request.Tenlop,
            Sophong = request.Phong
        };
        await _mediator.Send(command);
        return new UpdateLopHocResponse { Success = true };
    }

    public override async Task<DeleteLopHocResponse> DeleteLopHoc(
        DeleteLopHocRequest request, ServerCallContext context)
    {
        var command = new DeleteLopHocCommand { Id = request.Id };
        await _mediator.Send(command);
        return new DeleteLopHocResponse { Success = true };
    }
}