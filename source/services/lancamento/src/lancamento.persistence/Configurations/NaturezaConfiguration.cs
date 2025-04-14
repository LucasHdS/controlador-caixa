using Domain.Naturezas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;
internal class NaturezaConfiguration : IEntityTypeConfiguration<Natureza>
{
    public void Configure(EntityTypeBuilder<Natureza> builder)
    {
        builder.ToTable("T_NATUREZA", "lancamento");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("PK_NATUREZA")
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Descricao)
            .HasColumnName("TX_DESCRICAO")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.DataCadastro)
            .HasColumnName("DT_CADASTRO")
            .IsRequired();

        builder.Property(x => x.CodigoNatureza)
            .HasColumnName("CD_NATUREZA")
            .HasConversion<string>()
            .IsRequired();
    }
}
