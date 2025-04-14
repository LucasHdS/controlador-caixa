using Application.Abstractions.Cache;
using Application.Lancamentos;
using Infrastructure.Cache;
using Core.Abstractions.Messaging;
using EventBus;
using IntegrationEvents;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IEventBus>(provider =>
        {
            return new RabbitMqEventBus(
                serviceProvider: provider,
                hostName: Environment.GetEnvironmentVariable("RABBITMQ_HOST")!,
                user: Environment.GetEnvironmentVariable("RABBITMQ_USER")!,
                password: Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD")!
            );
        });

        services.AddScoped<IIntegrationEventHandler<LancamentoRealizadoIntegrationEvent>, LancamentoRealizadoIntegrationEventHandler>();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = Environment.GetEnvironmentVariable("REDIS_HOST")!;
        });

        services.AddSingleton(typeof(ICacheService<>), typeof(RedisCacheService<>));
    }
}