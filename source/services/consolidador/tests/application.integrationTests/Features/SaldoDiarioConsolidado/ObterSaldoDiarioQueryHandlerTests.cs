using Application.IntegrationTests.Shared;
using Application.Abstractions.Cache;
using Application.SaldoDiarioConsolidado.ObterSaldoDiario;
using Domain.SaldoDiarioConsolidado;

namespace Application.IntegrationTests.Features.SaldoDiarioConsolidado;
public class ObterSaldoDiarioQueryHandlerTests : BaseIntegrationTest
{
    private readonly ICacheService<SaldoDiario> _cacheService;
    public ObterSaldoDiarioQueryHandlerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
        _cacheService = GetService<ICacheService<SaldoDiario>>();
    }

    [Fact]
    public async Task Deve_Obter_Saldo_Diario_Existente()
    {
        // Arrange
        var data = DateOnly.FromDateTime(DateTime.Today);
        var saldo = new SaldoDiario(data, 250m);
        DbContext.SaldosDiarios.Add(saldo);
        await DbContext.SaveChangesAsync();
        
        var query = new ObterSaldoDiarioQuery(data);

        // Act
        var result = await Sender.Send(query);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(250m, result.Valor);
        Assert.Equal(saldo.Data, result.Data);
    }

    [Fact]
    public async Task Deve_Obter_Saldo_E_Salvar_No_Cache()
    {
        // Arrange
        var data = DateOnly.FromDateTime(DateTime.Today);
        var key = $"saldo:{data:yyyy-MM-dd}";
        var saldo = new SaldoDiario(data, 250m);
        DbContext.SaldosDiarios.Add(saldo);
        await DbContext.SaveChangesAsync();

        var query = new ObterSaldoDiarioQuery(data);

        // Act
        var result = await Sender.Send(query);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(250m, result.Valor);
        Assert.Equal(saldo.Data, result.Data);
        var saldoNoCache = await _cacheService.GetAsync(key);
        Assert.NotNull(saldoNoCache);
    }

    [Fact]
    public async Task Deve_Obter_Saldo_Diario_Existente_No_Cache()
    {
        // Arrange
        var data = DateOnly.FromDateTime(DateTime.Today);
        var key = $"saldo:{data:yyyy-MM-dd}";
        var saldo = new SaldoDiario(data, 250m);
        await _cacheService.SetAsync(key, saldo);

        var query = new ObterSaldoDiarioQuery(data);

        // Act
        var result = await Sender.Send(query);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(250m, result.Valor);
        Assert.Equal(saldo.Data, result.Data);
    }
}
