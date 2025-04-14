using Application.Abstractions.Cache;
using Domain.SaldoDiarioConsolidado;
using Core.Abstractions.Data;
using MediatR;

namespace Application.SaldoDiarioConsolidado.AtualizarSaldoDiario;
public class AtualizarSaldoDiarioCommandHandler(ISaldoDiarioRepository saldoDiarioRepository, IUnitOfWork unitOfWork, ICacheService<SaldoDiario> cache) : IRequestHandler<AtualizarSaldoDiarioCommand, Guid>
{
    private readonly ISaldoDiarioRepository _saldoDiarioRepository = saldoDiarioRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Guid> Handle(AtualizarSaldoDiarioCommand request, CancellationToken cancellationToken)
    {
        var cacheKey = $"saldo:{request.DataLancamento:yyyy-MM-dd}";



        var saldoDiario = await _saldoDiarioRepository.ObterSaldoDia(DateOnly.FromDateTime(request.DataLancamento.Date));

        if (saldoDiario is null)
            saldoDiario = CadastrarSaldo(request);
        else
            AtualizarSaldo(saldoDiario, request.Valor);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await cache.SetAsync(cacheKey, saldoDiario);

        return saldoDiario.Id;
    }

    private SaldoDiario CadastrarSaldo(AtualizarSaldoDiarioCommand request)
    {
        var saldoDiario = new SaldoDiario(DateOnly.FromDateTime(request.DataLancamento.Date), request.Valor);
        return _saldoDiarioRepository.Add(saldoDiario);
    }

    private SaldoDiario AtualizarSaldo(SaldoDiario saldoDiario, decimal valor)
    {
        saldoDiario.AtualizarSaldo(valor);
        return _saldoDiarioRepository.Update(saldoDiario);
    }
}