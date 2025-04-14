using Application.Abstractions.Cache;
using Application.Abstractions.Data;
using Domain.SaldoDiarioConsolidado;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.SaldoDiarioConsolidado.ObterSaldoDiario;

public class ObterSaldoDiarioQueryHandler(IApplicationDbContext context, ICacheService<SaldoDiario> cache) : IRequestHandler<ObterSaldoDiarioQuery, SaldoDiarioResponse>
{
    public async Task<SaldoDiarioResponse> Handle(ObterSaldoDiarioQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"saldo:{request.Data:yyyy-MM-dd}";

        var saldo = await cache.GetAsync(cacheKey);

        if (saldo is not null)
            return new SaldoDiarioResponse(saldo.Data, saldo.Valor);

        var saldoDiario = await context.SaldosDiarios
                                .AsNoTracking().
                                 FirstOrDefaultAsync(s => s.Data == request.Data, cancellationToken: cancellationToken) 
                                 ?? throw new SaldoDiarioNotFoundException(request.Data);

        await cache.SetAsync(cacheKey, saldoDiario);

        return new SaldoDiarioResponse(saldoDiario.Data, saldoDiario.Valor);
    }
}