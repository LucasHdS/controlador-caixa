using Core.Abstractions.Data;
using Domain.Lancamentos;
using Domain.TipoLancamentos;
using MediatR;

namespace Application.Lancamentos.RealizarLancamentoVenda;

internal sealed class RealizarLancamentoVendaCommandHandler(ITipoLancamentoRepository tipoLancamentoRepository,
                                                            ILancamentoRepository lancamentoRepository,
                                                            IUnitOfWork unitOfWork) : IRequestHandler<RealizarLancamentoVendaCommand, Guid>
{
    public async Task<Guid> Handle(RealizarLancamentoVendaCommand request, CancellationToken cancellationToken)
    {
        var tipoLancamento = tipoLancamentoRepository.Get(TipoLancamentoEnum.VENDA);
 
        var lancamento = lancamentoRepository.Add(new Lancamento(tipoLancamento, request.Valor));

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return lancamento.Id;
    }
}