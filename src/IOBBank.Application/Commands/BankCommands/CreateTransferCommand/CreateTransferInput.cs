using IOBBank.Core.Mediator.Commands;
using IOBBank.Domain.Enum;

namespace IOBBank.Application.Commands.BankCommands.CreateTransferCommand;

public class CreateTransferInput : CommandInput<CreateTransferResult>
{
    public double Value { get; set; }
    public Guid OriginBankAccountId { get; set; }
    public Guid DestinationBankAccountId { get; set; }
    public OperationType OperationType { get; set; }
}

