using Core.Common;
using Domain.Naturezas;

namespace Domain.TipoLancamentos;

public class TipoLancamento : Entity
{
    public Natureza Natureza { get; private set; }
    public Guid NaturezaId { get; private set; }
    public string Descricao { get; private set; }
    public TipoLancamentoEnum CodigoTipoLancamento { get; private set; }

    private TipoLancamento() { }
    public TipoLancamento(Natureza natureza, string descricao, TipoLancamentoEnum codigoTipoLancamento)
    {
        Natureza = natureza;
        Descricao = descricao;
        CodigoTipoLancamento = codigoTipoLancamento;
        DataCadastro = DateTime.UtcNow;
    }
    public TipoLancamento(Guid id,Guid naturezaId, string descricao, TipoLancamentoEnum codigoTipoLancamento, DateTime dataCadastro)
    {
        Id = id;
        NaturezaId = naturezaId;
        Descricao = descricao;
        CodigoTipoLancamento = codigoTipoLancamento;
        DataCadastro = dataCadastro;
    }

}
