using Consolidador.Domain.SaldoDiarioConsolidado;
using Microsoft.EntityFrameworkCore;

namespace Consolidador.Persistence.Repositories;
public class SaldoDiarioRepository(ApplicationDbContext context) : ISaldoDiarioRepository
{
    public SaldoDiario Add(SaldoDiario saldoDiario) => context.SaldosDiarios.Add(saldoDiario).Entity;
    public SaldoDiario Update(SaldoDiario saldoDiario) => context.SaldosDiarios.Update(saldoDiario).Entity;

    public async Task<SaldoDiario?> ObterSaldoDia(DateOnly data) => await context.SaldosDiarios.FirstOrDefaultAsync(s => s.Data == data);
}