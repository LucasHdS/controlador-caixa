﻿using Consolidador.Application.Abstractions.Data;
using Consolidador.Domain.SaldoDiarioConsolidado;
using Consolidador.Persistence.Repositories;
using Core.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Consolidador.Persistence;
public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options
                .UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION")));

        services.AddScoped<IApplicationDbContext>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IUnitOfWork>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ISaldoDiarioRepository, SaldoDiarioRepository>();

        return services;
    }
}