namespace IOBBank.Application.Queries.BankQueries.TransferHistoryPaginatedQuery;

public class TransferPaginatedQueryItem
{

    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Account { get; set; }
    public int Branch { get; set; }
    public double Balance { get; set; }
    public int OperationType { get; set; }
}