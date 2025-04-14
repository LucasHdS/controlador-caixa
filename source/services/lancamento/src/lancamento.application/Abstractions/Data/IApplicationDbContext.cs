using Domain.Lancamentos;
using Domain.Naturezas;
using Domain.TipoLancamentos;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;
public interface IApplicationDbContext
{
    DbSet<Lancamento> Lancamentos { get; set; }
    DbSet<TipoLancamento> TiposLancamento { get; set; }
    DbSet<Natureza> Naturezas { get; set; }
}
