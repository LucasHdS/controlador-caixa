using Core.Abstractions.Messaging;

namespace Application.SaldoDiarioConsolidado.AtualizarSaldoDiario;

public record AtualizarSaldoDiarioCommand(decimal Valor, DateTime DataLancamento) : ICommand<Guid>;