using Application.Lancamentos.RealizarLancamentoCompra;
using Application.Lancamentos.RealizarLancamentoVenda;
using Carter;
using MediatR;

namespace Api.Endpoints;
public class LancamentosEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/lancamento");

        group.MapPost("compra", RealizarLancamentoCompra);
        group.MapPost("venda", RealizarLancamentoVenda);
    }

    public static async Task<IResult> RealizarLancamentoCompra(
    RealizarLancamentoCompraRequest request,
    ISender sender)
    {
        var command = new RealizarLancamentoCompraCommand(request.Valor);

        await sender.Send(command);

        return Results.Ok();
    }

    public static async Task<IResult> RealizarLancamentoVenda(
    RealizarLancamentoVendaRequest request,
    ISender sender)
    {
        var command = new RealizarLancamentoVendaCommand(request.Valor);

        await sender.Send(command);

        return Results.Ok();
    }
}
