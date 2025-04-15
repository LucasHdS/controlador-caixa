using Application.Lancamentos.RealizarLancamentoCompra;
using Application.Lancamentos.RealizarLancamentoVenda;
using MediatR;

namespace Api.Endpoints;
public static class LancamentosEndpoints
{
    public static RouteGroupBuilder MapLancamentoEndpoints (this RouteGroupBuilder group)
    {
        group.MapPost("/compra", async (RealizarLancamentoCompraRequest request, ISender sender) =>
        {
            var command = new RealizarLancamentoCompraCommand(request.Valor);

            await sender.Send(command);

            return Results.Ok();
        });

        group.MapPost("/venda",async (RealizarLancamentoVendaRequest request, ISender sender) =>
        {
            var command = new RealizarLancamentoVendaCommand(request.Valor);

            await sender.Send(command);

            return Results.Ok();
        });

        return group;
    }
}
