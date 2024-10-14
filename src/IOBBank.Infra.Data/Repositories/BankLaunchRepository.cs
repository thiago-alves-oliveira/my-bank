using IOBBank.Domain.Entidades;
using IOBBank.Domain.Interfaces.Repositories;
using IOBBank.Infra.Data.Context;
using IOBBank.Infra.Data.Repositories.Base;

namespace IOBBank.Infra.Data.Repositories;
public class BankLaunchRepository : GenericRepository<BankLaunch>, IBankLaunchRepository
{
    public BankLaunchRepository(IOBBankContext context) : base(context)
    {
    }
}

