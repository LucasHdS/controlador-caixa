using Core.Abstractions.Data;
using Domain.Lancamentos;
using Domain.TipoLancamentos;
using MediatR;

namespace Application.Lancamentos.RealizarLancamentoCompra;

internal sealed class RealizarLancamentoCompraCommandHandler(ITipoLancamentoRepository tipoLancamentoRepository,
                                                            ILancamentoRepository lancamentoRepository,
                                                            IUnitOfWork unitOfWork) : IRequestHandler<RealizarLancamentoCompraCommand,Guid>
{
    public async Task<Guid> Handle(RealizarLancamentoCompraCommand request, CancellationToken cancellationToken)
    {
        var tipoLancamento = tipoLancamentoRepository.Get(TipoLancamentoEnum.COMPRA);

        var lancamento = lancamentoRepository.Add(new Lancamento(tipoLancamento, request.Valor));

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return lancamento.Id;
    }
}