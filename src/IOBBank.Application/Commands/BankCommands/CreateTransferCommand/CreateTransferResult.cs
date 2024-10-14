using IOBBank.Core.Mediator.Commands;

namespace IOBBank.Application.Commands.BankCommands.CreateTransferCommand;

public class CreateTransferResult : CommandResult
{
    public Guid Id { get; set; }
}