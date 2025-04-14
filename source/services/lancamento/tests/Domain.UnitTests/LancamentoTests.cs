using Domain.Lancamentos;
using Domain.Naturezas;
using Domain.TipoLancamentos;

namespace Domain.UnitTests;

public class LancamentoTests
{
    [Fact]
    public void NovaLancamento_DeveDeixarValorNegativo_QuantoTipoForCompra()
    {
        //Arrange
        var tipoLancamentoCompra = new TipoLancamento(new Natureza("debito", NaturezaEnum.DEBITO), "compra", TipoLancamentoEnum.COMPRA);
        var valor = 55m;
        //Act
        var lancamento = new Lancamento(tipoLancamentoCompra, valor);

        //Assert
        Assert.Equal(-valor, lancamento.Valor);
    }
}
