using Core.Abstractions.Messaging;

namespace Consolidador.Application.SaldoDiarioConsolidado.AtualizarSaldoDiario;

public record AtualizarSaldoDiarioCommand(decimal Valor, DateTime DataLancamento) : ICommand<Guid>;