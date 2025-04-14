using Consolidador.Domain.SaldoDiarioConsolidado;
using Consolidador.Application.SaldoDiarioConsolidado.AtualizarSaldoDiario;
using Moq;

namespace Application.UnitTests.SaldoDiarioConsolidado;

public class AtualizarSaldoDiarioCommandHandlerTests
{
    private readonly Mock<ISaldoDiarioRepository> _repositoryMock;
    private readonly AtualizarSaldoDiarioCommandHandler _handler;

    public AtualizarSaldoDiarioCommandHandlerTests()
    {
        _repositoryMock = new Mock<ISaldoDiarioRepository>();
        _handler = new AtualizarSaldoDiarioCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Deve_Criar_Saldo_Se_Nao_Existir()
    {
        // Arrange
        var data = new DateTime(2024, 04, 12);
        var valor = 100m;
        var command = new AtualizarSaldoDiarioCommand(valor, data);

        var novoSaldo = new SaldoDiario(DateOnly.FromDateTime(data), valor);

        _repositoryMock.Setup(r => r.ObterSaldoDia(DateOnly.FromDateTime(data))).ReturnsAsync((SaldoDiario?)null);
        _repositoryMock.Setup(r => r.Add(It.IsAny<SaldoDiario>())).Returns(novoSaldo);

        // Act
        var resultado = await _handler.Handle(command, default);

        // Assert
        Assert.Equal(novoSaldo.Id, resultado);
        _repositoryMock.Verify(r => r.Add(It.IsAny<SaldoDiario>()), Times.Once);
    }

    [Fact]
    public async Task Deve_Atualizar_Saldo_Existente()
    {
        // Arrange
        var data = new DateTime(2024, 04, 12);
        var dateOnly = DateOnly.FromDateTime(data);
        var valor = 50m;
        var valorLancamento = 100m;
        var valorEsperado = valorLancamento + valor;
        var command = new AtualizarSaldoDiarioCommand(valor, data);

        var saldoExistente = new SaldoDiario(dateOnly, valorLancamento);
        var idEsperado = saldoExistente.Id;

        _repositoryMock.Setup(r => r.ObterSaldoDia(dateOnly))
                       .ReturnsAsync(saldoExistente);

        _repositoryMock.Setup(r => r.Update(It.IsAny<SaldoDiario>()))
                       .Returns<SaldoDiario>(s => s); 

        // Act
        var resultado = await _handler.Handle(command, default);

        // Assert
        Assert.Equal(idEsperado, resultado);
        _repositoryMock.Verify(r => r.Update(It.Is<SaldoDiario>(s => s.Data == dateOnly && s.Valor == valorEsperado)), Times.Once);
    }
}