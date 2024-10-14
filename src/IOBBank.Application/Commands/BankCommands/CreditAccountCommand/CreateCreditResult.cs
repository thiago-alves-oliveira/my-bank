using IOBBank.Core.Mediator.Commands;

namespace IOBBank.Application.Commands.BankCommands.CreditCommand;

public class CreateCreditResult : CommandResult
{
    public Guid Id { get; set; }
}