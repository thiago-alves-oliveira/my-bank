using IOBBank.Core.Mediator.Commands;

namespace IOBBank.Application.Commands.BankCommands.CreditCommand;

public class CreateCreditInput : CommandInput<CreateCreditResult>
{
    public Guid BankAccountId { get; set; }
    public double Value { get; set; }
}

