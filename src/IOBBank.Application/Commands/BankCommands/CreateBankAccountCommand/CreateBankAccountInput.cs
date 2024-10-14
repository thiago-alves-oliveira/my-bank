using IOBBank.Core.Mediator.Commands;

namespace IOBBank.Application.Commands.BankCommands.CreateBankAccountCommand;

public class CreateBankAccountInput : CommandInput<CreateBankAccountResult>
{
    public string Name { get; set; }
    public int Account { get; set; }
    public int Branch { get; set; }
    public double Balance { get; set; }
}

