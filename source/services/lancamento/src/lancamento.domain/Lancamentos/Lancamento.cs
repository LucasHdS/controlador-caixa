using Core.Common;
using Domain.Naturezas;
using Domain.TipoLancamentos;

namespace Domain.Lancamentos;
public class Lancamento : Entity
{
    public TipoLancamento TipoLancamento { get; private set; }
    public Guid TipoLancamentoId { get; private set; }
    public decimal Valor { get; private set; }
    public Guid NumeroLancamento { get; private set; } = Guid.NewGuid();
    private Lancamento() { }

    public Lancamento(TipoLancamento tipoLancamento, decimal valor) : base()
    {
        TipoLancamentoId = tipoLancamento.Id;
        Valor = tipoLancamento.Natureza.CodigoNatureza == NaturezaEnum.CREDITO ? Math.Abs(valor) : -Math.Abs(valor);
        
        Raise(new LancamentoRealizadoDomainEvent(Id,NumeroLancamento,Valor));
    }
}
