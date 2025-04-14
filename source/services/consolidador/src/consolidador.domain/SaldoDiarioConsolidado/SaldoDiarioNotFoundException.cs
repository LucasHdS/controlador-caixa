namespace Consolidador.Domain.SaldoDiarioConsolidado;

public class SaldoDiarioNotFoundException(DateOnly data) : Exception($"Saldo do dia {data} nao encontrado");
