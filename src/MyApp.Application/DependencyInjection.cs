    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;

namespace MyApp.Application;

    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddResiliencePipeline("publish-retry", builder =>
        {
            builder.AddRetry(new RetryStrategyOptions
            {
                MaxRetryAttempts = 3,
                Delay = TimeSpan.FromSeconds(2),
                BackoffType = DelayBackoffType.Exponential,
                OnRetry = args =>
                {
                    Console.WriteLine($"Retry #{args.AttemptNumber} - Lỗi: {args.Outcome.Exception?.Message}");
                    return ValueTask.CompletedTask;
                }
            });
        });
            return services;
        }
    }