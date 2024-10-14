using IOBBank.Core.Mediator.Queries;
using IOBBank.Core.Notifications;
using IOBBank.Domain.Interfaces.Repositories;

namespace IOBBank.Application.Queries.BankQueries.BankAccountQuery;

public class BanckAccountQueryHandler : IQueryHandler<BanckAccountQueryInput, QueryListResult<BanckAccountQueryItem>>
{
    private readonly INotifier _notifier;
    private readonly IBankAccountRepository _bankAccountRepository;

    public BanckAccountQueryHandler(
        INotifier notifier
        , IBankAccountRepository bankAccountRepository)
    {
        _notifier = notifier;
        _bankAccountRepository = bankAccountRepository;
    }

    public  Task<QueryListResult<BanckAccountQueryItem>> Handle(BanckAccountQueryInput request, CancellationToken cancellationToken)
    {
        var listBankAccount = _bankAccountRepository
              .GetAsNoTracking()
              .Select(x => new BanckAccountQueryItem
              {
                  Id = x.Id,
                  BankName = x.Name,
              }).ToList();

        return Task.FromResult( new QueryListResult<BanckAccountQueryItem>
        {
            Result = listBankAccount
        });
    }
}