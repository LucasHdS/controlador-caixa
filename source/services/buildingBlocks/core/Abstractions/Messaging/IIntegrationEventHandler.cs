namespace Core.Abstractions.Messaging;

public interface IIntegrationEventHandler<T>
{
    Task HandleAsync(T @event, CancellationToken cancellationToken = default);
}