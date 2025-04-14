using Core.Common;

namespace Domain.SaldoDiarioConsolidado;
public class SaldoDiario : Entity
{
    public DateOnly Data { get; private set; }
    public decimal Valor { get; private set; }

    private SaldoDiario() { }

    public SaldoDiario(DateOnly data, decimal valor) : base()
    {
        Data = data;
        Valor = valor;
    }

    public void AtualizarSaldo(decimal valor)
    {
        Valor += valor;
    }
}

