using Domain.TipoLancamentos;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;
public class TipoLancamentoRepository(ApplicationDbContext context) : ITipoLancamentoRepository
{
    public TipoLancamento Get(TipoLancamentoEnum tipoLancamento) => context.TiposLancamento.AsNoTracking().Include(t => t.Natureza).First(l => l.CodigoTipoLancamento == tipoLancamento);
}