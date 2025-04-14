using Domain.Lancamentos;
using EventBus;
using IntegrationEvents;
using MediatR;

namespace Application.Lancamentos;
internal sealed class LancamentoRealizadoDomainEventHandler : INotificationHandler<LancamentoRealizadoDomainEvent>
{
    private readonly IEventBus _eventBus;

    public LancamentoRealizadoDomainEventHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public Task Handle(LancamentoRealizadoDomainEvent notification, CancellationToken cancellationToken)
    {
        _eventBus.Publish(new LancamentoRealizadoIntegrationEvent(notification.NumeroLancamento, notification.Valor, notification.DataLancamento), nameof(LancamentoRealizadoIntegrationEvent));

        return Task.CompletedTask;
    }
}

