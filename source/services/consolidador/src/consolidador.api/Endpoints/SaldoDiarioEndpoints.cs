using Carter;
using Consolidador.Application.SaldoDiarioConsolidado.ObterSaldoDiario;
using MediatR;

namespace Consolidador.Api.Endpoints;

public class SaldoDiarioEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/saldoDiario");

        group.MapGet("{data}", ConsultarSaldoDiario).WithName(nameof(ConsultarSaldoDiario));
    }
    public static async Task<IResult> ConsultarSaldoDiario(
    DateOnly data,
    ISender sender)
    {
        var query = new ObterSaldoDiarioQuery(data);

        var saldoDiario = await sender.Send(query);

        return Results.Ok(saldoDiario);
    }

}