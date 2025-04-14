using Application.IntegrationTests.Shared;
using Core.Abstractions.Messaging;
using IntegrationEvents;
using Microsoft.EntityFrameworkCore;

namespace Application.IntegrationTests.Features.Lancamentos;
public class LancamentoRealizadoIntegrationEventHandlerTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task Deve_Atualizar_Saldo_Ao_Receber_Evento_De_Lancamento()
    {
        // Arrange
        var handler = GetService<IIntegrationEventHandler<LancamentoRealizadoIntegrationEvent>>();

        var numeroLancamento = Guid.NewGuid();
        var data = DateTime.Today;
        var valor = 200m;
        var evento = new LancamentoRealizadoIntegrationEvent(numeroLancamento, valor, data);

        // Act
        await handler.HandleAsync(evento, default);

        // Assert
        var saldo = await DbContext.SaldosDiarios.FirstOrDefaultAsync(s => s.Data == DateOnly.FromDateTime(data));
        Assert.NotNull(saldo);
        Assert.Equal(valor, saldo.Valor);
    }
}
