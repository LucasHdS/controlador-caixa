using Domain.Lancamentos;

namespace Persistence.Repositories;
public class LancamentoRepository(ApplicationDbContext context) : ILancamentoRepository
{
    public Lancamento Add(Lancamento lancamento)
    {
        return context.Lancamentos.Add(lancamento).Entity;
    }
}