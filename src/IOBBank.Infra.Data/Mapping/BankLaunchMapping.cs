using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IOBBank.Domain.Entidades;
using IOBBank.Infra.Data.Mapping.Base;

namespace IOBBank.Infra.Data.Mapping;

public class BankLaunchMapping : EntidadeMapping<BankLaunch>
{
    public override void Configure(EntityTypeBuilder<BankLaunch> builder)
    {
        base.Configure(builder);

        builder.ToTable(ConstantesInfra.Tabelas.BankLaunch, ConstantesInfra.Schemas.Public);
        builder.HasKey(x => x.Id);

        builder.HasOne(bl => bl.BankAccount)
                     .WithMany()
                     .HasForeignKey(bl => bl.OriginBankAccountId)
                     .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(bl => bl.BankAccount)
              .WithMany()  
              .HasForeignKey(bl => bl.DestinationBankAccountId)
              .OnDelete(DeleteBehavior.Restrict); 

        builder.Property(bl => bl.OriginBankAccountId)
        .IsRequired();

        builder.Property(bl => bl.DestinationBankAccountId)
              .IsRequired();
    }
}