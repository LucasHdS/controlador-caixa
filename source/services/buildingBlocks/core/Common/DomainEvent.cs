using MediatR;

namespace Core.Common;
public record DomainEvent(Guid Id) : INotification;