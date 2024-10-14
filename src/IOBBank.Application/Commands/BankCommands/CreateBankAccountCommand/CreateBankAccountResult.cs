using IOBBank.Core.Mediator.Commands;

namespace IOBBank.Application.Commands.BankCommands.CreateBankAccountCommand;

public class CreateBankAccountResult : CommandResult
{
    public Guid Id { get; set; }
}