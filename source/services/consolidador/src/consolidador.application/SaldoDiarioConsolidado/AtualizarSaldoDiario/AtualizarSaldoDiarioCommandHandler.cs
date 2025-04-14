using Consolidador.Domain.SaldoDiarioConsolidado;
using Core.Abstractions.Data;
using MediatR;

namespace Consolidador.Application.SaldoDiarioConsolidado.AtualizarSaldoDiario;
public class AtualizarSaldoDiarioCommandHandler(ISaldoDiarioRepository saldoDiarioRepository, IUnitOfWork unitOfWork) : IRequestHandler<AtualizarSaldoDiarioCommand, Guid>
{
    private readonly ISaldoDiarioRepository _saldoDiarioRepository = saldoDiarioRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Guid> Handle(AtualizarSaldoDiarioCommand request, CancellationToken cancellationToken)
    {
        var saldoDiario = await _saldoDiarioRepository.ObterSaldoDia(DateOnly.FromDateTime(request.DataLancamento.Date));

        if (saldoDiario is null)
            saldoDiario = CadastrarSaldo(request);
        else
            AtualizarSaldo(saldoDiario, request.Valor);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    
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