using MediatR;

namespace Consolidador.Application.SaldoDiarioConsolidado.ObterSaldoDiario;
public record ObterSaldoDiarioQuery(DateOnly Data) : IRequest<SaldoDiarioResponse>;