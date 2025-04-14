using Consolidador.Domain.SaldoDiarioConsolidado;
using Microsoft.EntityFrameworkCore;

namespace Consolidador.Application.Abstractions.Data;
public interface IApplicationDbContext
{
    DbSet<SaldoDiario> SaldosDiarios { get; set; }
}
