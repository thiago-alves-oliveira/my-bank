using IOBBank.Core.Mediator.Commands;
using IOBBank.Core.Notifications;
using IOBBank.Domain.Entidades;
using IOBBank.Domain.Interfaces.Repositories;

namespace IOBBank.Application.Commands.BankCommands.DebitAccountCommand;

public class CreateDebitHandler : ICommandHandler<CreateDebitInput, CreateDebitResult>
{
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly IBankLaunchRepository _bankLaunchRepository;
    private readonly INotifier _notifier;

    public CreateDebitHandler(
        INotifier notifier,
        IBankAccountRepository bankAccountRepository,
        IBankLaunchRepository bankLaunchRepository)
    {
        _notifier = notifier;
        _bankAccountRepository = bankAccountRepository;
        _bankLaunchRepository = bankLaunchRepository;
    }

    public async Task<CreateDebitResult> Handle(CreateDebitInput request, CancellationToken cancellationToken)
    {
        var bankAccount = _bankAccountRepository.Get().Where(c => c.Id == request.BankAccountId).FirstOrDefault();

        if (bankAccount == null)
        {
            _notifier.Notify("Não foi possivel localizar dados da conta.");
            return new CreateDebitResult();
        }
        if (bankAccount.Balance < request.Value)
        {
            _notifier.Notify($"Não é possível efetuar o débito na conta, saldo insuficiente, Saldo atual {bankAccount.Balance}");
            return new CreateDebitResult();
        }

        bankAccount.Balance -= request.Value;

        var bankLaunch = new BankLaunch(request.Value, Domain.Enum.OperationType.Debit, bankAccount.Id, bankAccount.Id);

        _bankLaunchRepository.Add(bankLaunch);
        _bankAccountRepository.Update(bankAccount);
        await _bankAccountRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateDebitResult { };
    }
}
