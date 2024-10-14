using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IOBBank.Domain.Entidades;
using IOBBank.Infra.Data.Mapping.Base;

namespace IOBBank.Infra.Data.Mapping;

public class BankAccountMapping : EntidadeMapping<BankAccount>
{
    public override void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        base.Configure(builder);

        builder.ToTable(ConstantesInfra.Tabelas.BankAccount, ConstantesInfra.Schemas.Public);
        builder.HasKey(x => x.Id);       
    }
}