using EventBus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Persistence;

namespace Application.IntegrationTests.Shared;
public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected readonly ApplicationDbContext DbContext;
    protected readonly Mock<IEventBus> EventBusMock;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();

        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        EventBusMock = factory.EventBusMock;
    }
}