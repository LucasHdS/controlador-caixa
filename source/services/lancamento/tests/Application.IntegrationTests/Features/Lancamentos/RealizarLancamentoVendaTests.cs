using Application.IntegrationTests.Shared;
using Application.Lancamentos.RealizarLancamentoVenda;
using Domain.TipoLancamentos;
using IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Application.IntegrationTests.Features.Lancamentos;

public class RealizarLancamentoVendaTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task Realizar_DeveAdicionarLancamento_QuandoCommandChamada()
    {
        // Arrange
        var valor = 99.99m;
        var command = new RealizarLancamentoVendaCommand(valor);

        // Act
        var lancamentoId = await Sender.Send(command);

        // Assert
        var lancamento = DbContext.Lancamentos.AsNoTracking().Include(lancamento => lancamento.TipoLancamento).FirstOrDefault(p => p.Id == lancamentoId);

        Assert.NotNull(lancamento);
        Assert.Equal(TipoLancamentoEnum.VENDA, lancamento.TipoLancamento.CodigoTipoLancamento);
        Assert.Equal(valor, lancamento.Valor);

        EventBusMock.Verify(e => e.Publish(
        It.Is<LancamentoRealizadoIntegrationEvent>(ev =>
        ev.NumeroLancamento == lancamento.NumeroLancamento &&
        ev.DataLancamento.Date == DateTime.Today &&
        ev.Valor == valor), nameof(LancamentoRealizadoIntegrationEvent)), Times.Once);
    }
}