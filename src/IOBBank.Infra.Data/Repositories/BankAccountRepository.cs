using IOBBank.Domain.Entidades;
using IOBBank.Domain.Interfaces.Repositories;
using IOBBank.Infra.Data.Context;
using IOBBank.Infra.Data.Repositories.Base;

namespace IOBBank.Infra.Data.Repositories;
public class BankAccountRepository : GenericRepository<BankAccount>, IBankAccountRepository
{
    public BankAccountRepository(IOBBankContext context) : base(context)
    {
    }
}

