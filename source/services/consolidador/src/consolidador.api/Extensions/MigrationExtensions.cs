using EventBus;
using IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Api.Extensions;
public static class MigrationExtensions
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.Migrate();
    }
    public static void ConfigureIntegrationEventHandlers(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();

        eventBus.Subscribe<LancamentoRealizadoIntegrationEvent>(nameof(LancamentoRealizadoIntegrationEvent));
    }
}
