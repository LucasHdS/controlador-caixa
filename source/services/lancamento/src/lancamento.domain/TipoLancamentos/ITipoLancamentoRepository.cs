namespace Domain.TipoLancamentos;

public interface ITipoLancamentoRepository
{
    TipoLancamento Get(TipoLancamentoEnum tipoLancamento);
}
