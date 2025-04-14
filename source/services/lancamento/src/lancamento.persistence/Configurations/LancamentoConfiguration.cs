using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Lancamentos;

namespace Persistence.Configurations;
internal class LancamentoConfiguration : IEntityTypeConfiguration<Lancamento>
{
    public void Configure(EntityTypeBuilder<Lancamento> builder)
    {
        builder.ToTable("T_LANCAMENTO", "lancamento");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("PK_LANCAMENTO")
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Valor)
            .HasColumnName("VALOR")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.DataCadastro)
            .HasColumnName("DT_CADASTRO")
            .IsRequired();

        builder.Property(x => x.NumeroLancamento)
            .HasColumnName("NUM_LANCAMENTO")
            .IsRequired();

        builder.Property(x => x.TipoLancamentoId)
            .HasColumnName("FK_TP_LANCAMENTO")
            .IsRequired();

        builder.HasOne(x => x.TipoLancamento)
            .WithMany()
            .HasForeignKey(x => x.TipoLancamentoId);
    }
}
