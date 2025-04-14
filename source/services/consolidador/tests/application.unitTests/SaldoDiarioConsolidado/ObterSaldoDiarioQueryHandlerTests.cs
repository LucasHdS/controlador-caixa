using Consolidador.Application.Abstractions.Data;
using Consolidador.Application.SaldoDiarioConsolidado.ObterSaldoDiario;
using Consolidador.Domain.SaldoDiarioConsolidado;
using MockQueryable.Moq;
using Moq;

namespace Application.UnitTests.SaldoDiarioConsolidado;
public class ObterSaldoDiarioQueryHandlerTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    private readonly ObterSaldoDiarioQueryHandler _handler;

    public ObterSaldoDiarioQueryHandlerTests()
    {
        _contextMock = new Mock<IApplicationDbContext>();
        _handler = new ObterSaldoDiarioQueryHandler(_contextMock.Object);
    }

    [Fact]
    public async Task Deve_Retornar_Saldo_Quando_Encontrado()
    {
        // Arrange
        var data = new DateOnly(2024, 04, 12);
        var valorSaldo = 200m;
        var saldo = new SaldoDiario(data, valorSaldo);

        var dataSetMock = new List<SaldoDiario> { saldo }.AsQueryable().BuildMockDbSet();

        _contextMock.Setup(c => c.SaldosDiarios).Returns(dataSetMock.Object);

        var query = new ObterSaldoDiarioQuery(data);

        // Act
        var result = await _handler.Handle(query, default);

        // Assert
        Assert.Equal(valorSaldo, result.Valor);
        Assert.Equal(saldo.Data, result.Data);
    }

    [Fact]
    public async Task Deve_Lancar_Excecao_Quando_Saldo_Nao_Encontrado()
    {
        // Arrange
        var data = new DateOnly(2024, 04, 12);
        var emptySet = new List<SaldoDiario>().AsQueryable().BuildMockDbSet();

        _contextMock.Setup(c => c.SaldosDiarios).Returns(emptySet.Object);

        var query = new ObterSaldoDiarioQuery(data);

        // Act & Assert
        await Assert.ThrowsAsync<SaldoDiarioNotFoundException>(() =>
            _handler.Handle(query, default));
    }
}
