using IOBBank.Core.Mediator.Queries;
using IOBBank.Domain.Enum;

namespace IOBBank.Application.Queries.BankQueries.TransferHistoryPaginatedQuery;

public class TransferPaginatedQueryInput
    : QueryPaginatedInput<QueryPaginatedResult<TransferPaginatedQueryItem>>
{
    public Guid? BankAccountId { get; set; }
    public OperationType? OperationType { get; set; }
}