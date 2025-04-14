using Consolidador.Domain.SaldoDiarioConsolidado;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Consolidador.Persistence.Configurations;
internal class SaldoDiarioConfiguration : IEntityTypeConfiguration<SaldoDiario>
{
    public void Configure(EntityTypeBuilder<SaldoDiario> builder)
    {
        builder.ToTable("T_SALDO_DIARIO", "consolidado");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("PK_SALDO_DIARIO")
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Data)
            .HasColumnName("DT_SALDO")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(x => x.Valor)
            .HasColumnName("VALOR")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.DataCadastro)
            .HasColumnName("DT_CADASTRO")
            .IsRequired();
    }
}
