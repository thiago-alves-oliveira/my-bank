using IOBBank.Core.Mediator.Queries;
using IOBBank.Core.Notifications;
using IOBBank.Domain.Interfaces.Repositories;

namespace IOBBank.Application.Queries.BankQueries.BalanceAccountQuery;

public class BalanceAccountQueryHandler : IQueryHandler<BalanceAccountQueryInput, QueryResult<BalanceAccountQueryItem>>
{
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly INotifier _notifier;

    public BalanceAccountQueryHandler(
        INotifier notifier
      , IBankAccountRepository bankAccountRepository)
    {
        _notifier = notifier;
        _bankAccountRepository = bankAccountRepository;
    }
    public async Task<QueryResult<BalanceAccountQueryItem>> Handle(BalanceAccountQueryInput request, CancellationToken cancellationToken)
    {        
        var bankAccount = _bankAccountRepository
            .GetAsNoTracking()
            .Where(c => c.Id == request.BankAccountId)
            .FirstOrDefault();

        if (bankAccount == null) 
        {
            _notifier.Notify("Não foi possivel localizar os dados da contas em nossa base");
            return new QueryResult<BalanceAccountQueryItem>();
        }

        return new BalanceAccountQueryItem()
        {
           Balance = bankAccount.Balance,
        };
    }
}