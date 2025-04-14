using Core.Common;

namespace Domain.Naturezas;
public class Natureza : Entity
{
    public string Descricao { get; private set; }
    public NaturezaEnum CodigoNatureza { get; private set; }
    private Natureza() { }

    public Natureza(string desricao, NaturezaEnum codigoNatureza)
    {
        Descricao = desricao;
        CodigoNatureza = codigoNatureza;
        DataCadastro = DateTime.UtcNow;
    }
    public Natureza(Guid id, string desricao, NaturezaEnum codigoNatureza, DateTime dataCadastro)
    {
        Id = id;
        Descricao = desricao;
        CodigoNatureza = codigoNatureza;
        DataCadastro = dataCadastro;
    }
}
