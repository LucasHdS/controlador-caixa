using Core.Common;

namespace Domain.Lancamentos;
public record LancamentoRealizadoDomainEvent (Guid Id, Guid NumeroLancamento, decimal Valor, DateTime DataLancamento): DomainEvent(Id);