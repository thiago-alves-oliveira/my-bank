using IOBBank.Core.Mediator.Commands;

namespace IOBBank.Application.Commands.BankCommands.DebitAccountCommand;

public class CreateDebitResult : CommandResult
{
    public Guid Id { get; set; }
}