using Domain.SaldoDiarioConsolidado;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;
public interface IApplicationDbContext
{
    DbSet<SaldoDiario> SaldosDiarios { get; set; }
}
