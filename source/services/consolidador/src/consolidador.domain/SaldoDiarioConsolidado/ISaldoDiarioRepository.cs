namespace Domain.SaldoDiarioConsolidado;

public interface ISaldoDiarioRepository
{
    SaldoDiario Add(SaldoDiario saldoDiario);
    SaldoDiario Update(SaldoDiario saldoDiario);
    Task<SaldoDiario?> ObterSaldoDia(DateOnly data);
}