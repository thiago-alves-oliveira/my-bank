using ChoETL;
using IOBBank.Core.Extensions;
using IOBBank.Core.Mediator.Queries;
using IOBBank.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IOBBank.Application.Queries.BankQueries.TransferHistoryPaginatedQuery;

public class ObterCategoriaPaginatedQueryHandler : IQueryHandler<TransferPaginatedQueryInput, QueryPaginatedResult<TransferPaginatedQueryItem>>
{
    private readonly IBankLaunchRepository _bankLaunchRepository;

    public ObterCategoriaPaginatedQueryHandler(IBankLaunchRepository bankLaunchRepository)
    {
        _bankLaunchRepository = bankLaunchRepository;
    }

    public async Task<QueryPaginatedResult<TransferPaginatedQueryItem>> Handle(TransferPaginatedQueryInput request, CancellationToken cancellationToken)
    {
        var query = _bankLaunchRepository.GetAsNoTracking()
            .Include(c => c.BankAccount)
            .AsQueryable();

        if (request.BankAccountId != null)        
            query = query.Where(c => c.BankAccount.Id == request.BankAccountId);        

        if (request.OperationType != null)        
            query = query.Where(c => c.OperationType == request.OperationType);       

        var (listHistoryTransfer, pagination) = await query
                   .Select(x => new TransferPaginatedQueryItem
                   {
                       Id = x.Id,
                       Balance = x.BankAccount.Balance,
                       OperationType = (int)x.OperationType,
                       Account = x.BankAccount.Account,
                       Branch = x.BankAccount.Branch,
                       Name = x.BankAccount.Name
                   })
               .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);

        return new QueryPaginatedResult<TransferPaginatedQueryItem>
        {
            Pagination = pagination,
            Result = listHistoryTransfer
        };
    }
}