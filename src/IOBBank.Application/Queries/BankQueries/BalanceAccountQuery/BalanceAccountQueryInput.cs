using IOBBank.Core.Mediator.Queries;

namespace IOBBank.Application.Queries.BankQueries.BalanceAccountQuery;

public class BalanceAccountQueryInput
    : QueryInput<QueryResult<BalanceAccountQueryItem>>
{
    public Guid BankAccountId { get; set; }
}
