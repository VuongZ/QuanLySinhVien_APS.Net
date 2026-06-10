using System.Reflection;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Domain.Repositories;
using MyApp.Infrastructure.Consumers;
using MyApp.Infrastructure.MongoDB;
using MyApp.Infrastructure.MongoDB.Repositories;
using MyApp.Infrastructure.MongoDB.RepoSitories;
using MyApp.Infrastructure.Persistence;
using MyApp.Infrastructure.Persistence.Repositories;

namespace MyApp.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInFrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
            configuration.GetConnectionString("Default")
        ));
         services.AddSingleton<MongoDbContext>();
         services.AddScoped<IUnitOfWork, UnitOfWork>();


        services.AddScoped<ISinhVienRepository, SinhVienRepository>();
        services.AddScoped<ILopHocRepository, LopHocRepository>();
         services.AddScoped<ISinhVienMongoRepository, SinhVienMongoRepository>();
        services.AddScoped<ILopHocMongoRepository, LopHocMongoRepository>();
         services.AddMassTransit(x =>
        {
            // Đăng ký Consumer
            x.AddConsumer<SinhVienCreatedConsumer>();
            x.AddConsumer<LopHocCreatedConsumer>();
            x.AddConsumer<SinhVienDeletedConsumer>();
            x.AddConsumer<LopHocDeletedConsumer>();
            x.AddConsumer<SinhVienUpdatedConsumer>();
            x.AddConsumer<LopHocUpdatedConsumer>();
          

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(configuration["RabbitMQ:Host"], "/", h =>
                {
                    h.Username(configuration["RabbitMQ:Username"] ?? "guest");
                    h.Password(configuration["RabbitMQ:Password"] ?? "guest");
                });

                // Đăng ký queue cho từng Consumer
                cfg.ReceiveEndpoint("sinhvien-created", e =>
                {
                    e.ConfigureConsumer<SinhVienCreatedConsumer>(ctx);
                });

                cfg.ReceiveEndpoint("lophoc-created", e =>
                {
                    e.ConfigureConsumer<LopHocCreatedConsumer>(ctx);
                });
                cfg.ReceiveEndpoint("sinhvien-deleted", e =>
                    e.ConfigureConsumer<SinhVienDeletedConsumer>(ctx));

                cfg.ReceiveEndpoint("lophoc-deleted", e =>
                    e.ConfigureConsumer<LopHocDeletedConsumer>(ctx));

                cfg.ReceiveEndpoint("sinhvien-updated", e =>
                    e.ConfigureConsumer<SinhVienUpdatedConsumer>(ctx));

                cfg.ReceiveEndpoint("lophoc-updated", e =>
                    e.ConfigureConsumer<LopHocUpdatedConsumer>(ctx));
            });
        });
        return services;
        
    }
}
