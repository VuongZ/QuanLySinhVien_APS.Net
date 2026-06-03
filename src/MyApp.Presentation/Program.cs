using MyApp.Infrastructure;
using MyApp.Infrastructure.Persistence;
using MyApp.Application;
using MyApp.Presentation.Services;
var builder = WebApplication.CreateBuilder(args);

// Add gRPC
builder.Services.AddGrpc();
builder.Services.AddApplication();     
builder.Services.AddInFrastructure(builder.Configuration);
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (db.Database.CanConnect())
    {
        Console.WriteLine("Kết nối database thành công!");
    }
    else
    {
        Console.WriteLine("Kết nối database thất bại!");
    }
}
app.MapGrpcService<SinhVienGrpcService>();
app.MapGrpcService<LopHocGrpcService>();
app.MapGet("/", () => "gRPC Server is running!");

app.Run();