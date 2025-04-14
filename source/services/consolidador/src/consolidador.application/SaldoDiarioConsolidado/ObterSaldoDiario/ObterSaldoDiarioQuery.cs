using MediatR;

namespace Application.SaldoDiarioConsolidado.ObterSaldoDiario;
public record ObterSaldoDiarioQuery(DateOnly Data) : IRequest<SaldoDiarioResponse>;