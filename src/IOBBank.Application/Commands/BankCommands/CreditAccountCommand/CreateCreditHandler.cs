using IOBBank.Core.Mediator.Commands;
using IOBBank.Core.Notifications;
using IOBBank.Domain.Entidades;
using IOBBank.Domain.Interfaces.Repositories;
using Newtonsoft.Json.Linq;

namespace IOBBank.Application.Commands.BankCommands.CreditCommand;

public class CreateCreditHandler : ICommandHandler<CreateCreditInput, CreateCreditResult>
{
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly IBankLaunchRepository _bankLaunchRepository;
    private readonly INotifier _notifier;

    public CreateCreditHandler(
        INotifier notifier,
        IBankAccountRepository bankAccountRepository,
        IBankLaunchRepository bankLaunchRepository)
    {
        _notifier = notifier;
        _bankAccountRepository = bankAccountRepository;
        _bankLaunchRepository = bankLaunchRepository;
    }

    public async Task<CreateCreditResult> Handle(CreateCreditInput request, CancellationToken cancellationToken)
    {
        var bankAccount = _bankAccountRepository.Get().Where(c => c.Id == request.BankAccountId).FirstOrDefault();

        if (bankAccount == null)
        {
            _notifier.Notify("Não foi possivel localizar dados da conta.");
            return new CreateCreditResult();
        }

        bankAccount.Balance += request.Value;

        var bankLaunch = new BankLaunch(request.Value, Domain.Enum.OperationType.Credit, bankAccount.Id, bankAccount.Id);

        _bankLaunchRepository.Add(bankLaunch);

        _bankAccountRepository.Update(bankAccount);
        await _bankAccountRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateCreditResult { };
    }
}
