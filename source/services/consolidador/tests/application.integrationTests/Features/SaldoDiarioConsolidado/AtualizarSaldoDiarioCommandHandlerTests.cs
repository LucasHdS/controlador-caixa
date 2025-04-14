using Application.IntegrationTests.Shared;
using Application.Abstractions.Cache;
using Application.SaldoDiarioConsolidado.AtualizarSaldoDiario;
using Domain.SaldoDiarioConsolidado;
using Microsoft.EntityFrameworkCore;

namespace Application.IntegrationTests.Features.SaldoDiarioConsolidado;
public class AtualizarSaldoDiarioCommandHandlerTests : BaseIntegrationTest
{
    private readonly ICacheService<SaldoDiario> _cacheService;
    public AtualizarSaldoDiarioCommandHandlerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
        _cacheService = GetService<ICacheService<SaldoDiario>>();
    }

    [Fact]
    public async Task Deve_Criar_Registro_De_Saldo_Diario()
    {
        // Arrange
        var data = DateTime.Today;
        var dateOnly = DateOnly.FromDateTime(data);
        var key = $"saldo:{dateOnly:yyyy-MM-dd}";
        var valor = 150m;
        var command = new AtualizarSaldoDiarioCommand(valor, data);

        // Act
        var result = await Sender.Send(command);

        // Assert
        var saldo = await DbContext.SaldosDiarios.FirstOrDefaultAsync(s => s.Data == dateOnly);
        Assert.NotNull(saldo);
        Assert.Equal(valor, saldo.Valor);
        Assert.Equal(result, saldo.Id);
        var saldoNoCache = await _cacheService.GetAsync(key);
        Assert.NotNull(saldoNoCache);
    }
}
