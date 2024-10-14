using IOBBank.Core.Mediator.Commands;
using IOBBank.Core.Notifications;
using IOBBank.Domain.Entidades;
using IOBBank.Domain.Interfaces.Repositories;

namespace IOBBank.Application.Commands.BankCommands.CreateBankAccountCommand;

public class CreateBankAccountHandler : ICommandHandler<CreateBankAccountInput, CreateBankAccountResult>
{
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly INotifier _notifier;

    public CreateBankAccountHandler(
        INotifier notifier,
        IBankAccountRepository bankAccountRepository)
    {
        _notifier = notifier;
        _bankAccountRepository = bankAccountRepository;
    }

    public async Task<CreateBankAccountResult> Handle(CreateBankAccountInput request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Name))        
            _notifier.Notify("Obrigatorio enviar nome do banco");        

        var bankAccount = new BankAccount(request.Name, 
            request.Account, request.Branch, request.Balance);

        _bankAccountRepository.Add(bankAccount);
        await _bankAccountRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateBankAccountResult { Id = bankAccount.Id };
    }
}
