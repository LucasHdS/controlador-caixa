using Consolidador.Application.Abstractions.Data;
using Consolidador.Domain.SaldoDiarioConsolidado;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Consolidador.Application.SaldoDiarioConsolidado.ObterSaldoDiario;

public class ObterSaldoDiarioQueryHandler(IApplicationDbContext context) : IRequestHandler<ObterSaldoDiarioQuery, SaldoDiarioResponse>
{
    public async Task<SaldoDiarioResponse> Handle(ObterSaldoDiarioQuery request, CancellationToken cancellationToken)
    {
        var saldoDiario = await context.SaldosDiarios
                                .AsNoTracking().
                                 FirstOrDefaultAsync(s => s.Data == request.Data, cancellationToken: cancellationToken) 
                                 ?? throw new SaldoDiarioNotFoundException(request.Data);

        return new SaldoDiarioResponse(saldoDiario.Data, saldoDiario.Valor);
    }
}