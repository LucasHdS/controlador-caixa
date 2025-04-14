namespace IntegrationEvents;
public record LancamentoRealizadoIntegrationEvent(Guid NumeroLancamento, decimal Valor, DateTime DataLancamento);