﻿using EventBus;
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

    }
}