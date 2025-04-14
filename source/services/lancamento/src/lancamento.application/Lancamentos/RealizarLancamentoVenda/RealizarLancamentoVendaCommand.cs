using Core.Abstractions.Messaging;
using MediatR;

namespace Application.Lancamentos.RealizarLancamentoVenda;

public record RealizarLancamentoVendaCommand(decimal Valor) : ICommand<Guid>;

