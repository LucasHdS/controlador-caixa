using Core.Abstractions.Messaging;

namespace Application.Lancamentos.RealizarLancamentoCompra;

public record RealizarLancamentoCompraCommand(decimal Valor) : ICommand<Guid>;