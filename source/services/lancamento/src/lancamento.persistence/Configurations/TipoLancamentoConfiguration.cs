using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.TipoLancamentos;

namespace Persistence.Configurations;
internal class TipoLancamentoConfiguration : IEntityTypeConfiguration<TipoLancamento>
{
    public void Configure(EntityTypeBuilder<TipoLancamento> builder)
    {
        builder.ToTable("T_TIPO_LANCAMENTO", "lancamento");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("PK_TP_LANCAMENTO")
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Descricao)
            .HasColumnName("TX_DESCRICAO")
            .IsRequired();

        builder.Property(x => x.CodigoTipoLancamento)
            .HasColumnName("CD_TIPO_LANCAMENTO")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.DataCadastro)
            .HasColumnName("DT_CADASTRO")
            .IsRequired();

        builder.Property(x => x.NaturezaId)
            .HasColumnName("FK_NATUREZA")
            .IsRequired();

        builder.HasOne(x => x.Natureza)
            .WithMany()
            .HasForeignKey(x => x.NaturezaId);
    }
}