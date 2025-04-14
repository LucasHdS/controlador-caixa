using Application.Abstractions.Data;
using Core.Abstractions.Data;
using Domain.Lancamentos;
using Domain.TipoLancamentos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence;
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

        services.AddScoped<ITipoLancamentoRepository, TipoLancamentoRepository>();

        services.AddScoped<ILancamentoRepository, LancamentoRepository>();

        return services;
    }
}
