using Application.SaldoDiarioConsolidado.ObterSaldoDiario;
using MediatR;

namespace Api.Endpoints;

public static class SaldoDiarioEndpoints
{
    public static RouteGroupBuilder MapSaldoDiarioEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("{data}", ConsultarSaldoDiario);

        return group;
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