using Core.Abstractions.Data;
using Core.Common;
using Domain.Lancamentos;
using Domain.Naturezas;
using Domain.TipoLancamentos;
using Application.Abstractions.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Persistence;
public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;

    public ApplicationDbContext(DbContextOptions options, IPublisher publisher)
        : base(options)
    {
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        var debitoId = Guid.Parse("08a6ac05-1f1c-41cb-adc2-2113820719e0");
        var creditoId = Guid.Parse("cc934f0b-7ac0-43c4-9d50-7c2fc4b03d72");

        modelBuilder.Entity<Natureza>().HasData(
            new Natureza(debitoId, "Débito", NaturezaEnum.DEBITO, new DateTime(2025, 04, 13, 0, 0, 0, DateTimeKind.Utc)),
            new Natureza(creditoId, "Crédito", NaturezaEnum.CREDITO, new DateTime(2025, 04, 13, 0, 0, 0, DateTimeKind.Utc))
        );
        modelBuilder.Entity<TipoLancamento>().HasData(
            new TipoLancamento(Guid.Parse("94c0f65a-661f-4656-beac-8bff9f8ea7f6"), debitoId, "Compra", TipoLancamentoEnum.COMPRA, new DateTime(2025,04,13, 0, 0, 0, DateTimeKind.Utc)),
            new TipoLancamento(Guid.Parse("04d97159-bcf6-4cfb-b271-64917b800125"), creditoId, "Venda", TipoLancamentoEnum.VENDA, new DateTime(2025, 04, 13, 0, 0, 0, DateTimeKind.Utc))
        );
    }

    public DbSet<Lancamento> Lancamentos { get; set; }
    public DbSet<TipoLancamento> TiposLancamento { get; set; }
    public DbSet<Natureza> Naturezas { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var domainEvents = ChangeTracker.Entries<Entity>()
            .Select(e => e.Entity)
            .Where(e => e.GetDomainEvents().Any())
            .SelectMany(e =>
            {
                var domainEvents = e.GetDomainEvents();

                e.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        return result;
    }
}
