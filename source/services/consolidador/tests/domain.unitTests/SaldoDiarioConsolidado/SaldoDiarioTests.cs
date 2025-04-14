using Domain.SaldoDiarioConsolidado;

namespace Domain.UnitTests.SaldoDiarioConsolidado;
public class SaldoDiarioTests
{
    [Fact]
    public void Deve_Criar_SaldoDiario_Com_Valores_Corretos()
    {
        // Arrange
        var data = new DateOnly(2024, 04, 12);
        var valor = 100m;

        // Act
        var saldo = new SaldoDiario(data, valor);

        // Assert
        Assert.Equal(data, saldo.Data);
        Assert.Equal(valor, saldo.Valor);
        Assert.NotEqual(Guid.Empty, saldo.Id);
    }

    [Fact]
    public void Deve_Atualizar_Valor_Com_Sucesso()
    {
        // Arrange
        var saldoInicial = 200m;
        var saldo = new SaldoDiario(new DateOnly(2024, 04, 12), saldoInicial);
        var valor1 = 50m;
        var valor2 = -25m;
        var saldoEsperado = saldoInicial + valor1 + valor2;

        // Act
        saldo.AtualizarSaldo(valor1);
        saldo.AtualizarSaldo(valor2);

        // Assert
        Assert.Equal(saldoEsperado, saldo.Valor);
    }
}
