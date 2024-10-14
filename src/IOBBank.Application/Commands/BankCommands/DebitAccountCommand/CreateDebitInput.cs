using IOBBank.Core.Mediator.Commands;

namespace IOBBank.Application.Commands.BankCommands.DebitAccountCommand;

public class CreateDebitInput : CommandInput<CreateDebitResult>
{
    public Guid BankAccountId { get; set; }
    public double Value { get; set; }
}

