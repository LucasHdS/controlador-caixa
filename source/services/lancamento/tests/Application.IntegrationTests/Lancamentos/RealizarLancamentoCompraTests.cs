using Application.Lancamentos.RealizarLancamentoCompra;
using Domain.TipoLancamentos;
using Microsoft.EntityFrameworkCore;

namespace Application.IntegrationTests.Lancamentos;

public class RealizarLancamentoCompraTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task Realizar_DeveAdicionarLancamento_QuandoCommandChamada()
    {
        // Arrange
        var valor = 99.99m;
        var command = new RealizarLancamentoCompraCommand(valor);

        // Act
        var lancamentoId = await Sender.Send(command);

        // Assert
        var lancamento = DbContext.Lancamentos.AsNoTracking().Include(lancamento => lancamento.TipoLancamento).FirstOrDefault(p => p.Id == lancamentoId);

        Assert.NotNull(lancamento);
        Assert.Equal(TipoLancamentoEnum.COMPRA, lancamento.TipoLancamento.CodigoTipoLancamento);
        Assert.Equal(-valor, lancamento.Valor);
    }
}
