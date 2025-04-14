using Application.SaldoDiarioConsolidado.AtualizarSaldoDiario;
using Core.Abstractions.Messaging;
using IntegrationEvents;
using MediatR;

namespace Application.Lancamentos;

public class LancamentoRealizadoIntegrationEventHandler(ISender sender) : IIntegrationEventHandler<LancamentoRealizadoIntegrationEvent>
{
    public async Task HandleAsync(LancamentoRealizadoIntegrationEvent @event, CancellationToken cancellationToken = default)
    {
        await sender.Send(new AtualizarSaldoDiarioCommand(@event.Valor, @event.DataLancamento), cancellationToken);
    }
}