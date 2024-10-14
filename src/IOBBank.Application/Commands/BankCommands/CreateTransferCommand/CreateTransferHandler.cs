using IOBBank.Core.Mediator.Commands;
using IOBBank.Core.Notifications;
using IOBBank.Domain.Entidades;
using IOBBank.Domain.Enum;
using IOBBank.Domain.Interfaces.Repositories;

namespace IOBBank.Application.Commands.BankCommands.CreateTransferCommand;

public class CreateTransferHandler : ICommandHandler<CreateTransferInput, CreateTransferResult>
{
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly IBankLaunchRepository _bankLaunchRepository;
    private readonly INotifier _notifier;

    public CreateTransferHandler(
        INotifier notifier,
        IBankAccountRepository bankAccountRepository,
        IBankLaunchRepository bankLaunchRepository = null)
    {
        _notifier = notifier;
        _bankAccountRepository = bankAccountRepository;
        _bankLaunchRepository = bankLaunchRepository;
    }


    public async Task<CreateTransferResult> Handle(CreateTransferInput request, CancellationToken cancellationToken)
    {
        var originBankAccount = _bankAccountRepository.Get().FirstOrDefault(c => c.Id == request.OriginBankAccountId);
        if (originBankAccount == null)
        {
            _notifier.Notify("Não foi possível localizar a conta bancária de origem.");
            return new CreateTransferResult();
        }

        var destinationBankAccount = _bankAccountRepository.Get().FirstOrDefault(c => c.Id == request.DestinationBankAccountId);
        if (destinationBankAccount == null)
        {
            _notifier.Notify("Não foi possível localizar a conta bancária de destino.");
            return new CreateTransferResult();
        }

        switch (request.OperationType)
        {
            case OperationType.Debit:
                if (originBankAccount.Balance < request.Value)
                {
                    _notifier.Notify("Saldo insuficiente para realizar o débito.");
                    return new CreateTransferResult();
                }

                originBankAccount.Balance -= request.Value;
                destinationBankAccount.Balance += request.Value;

                var bankLaunchDebit = new BankLaunch(request.Value, OperationType.Debit,
                    originBankAccount.Id, destinationBankAccount.Id);
                _bankLaunchRepository.Add(bankLaunchDebit);
                break;

            case OperationType.Credit:
                originBankAccount.Balance += request.Value;
                destinationBankAccount.Balance -= request.Value;
                var bankLaunchCredit = new BankLaunch(request.Value, OperationType.Credit,
                    originBankAccount.Id, destinationBankAccount.Id);
                _bankLaunchRepository.Add(bankLaunchCredit);
                break;

            case OperationType.Transfer:
                if (originBankAccount.Balance < request.Value)
                {
                    _notifier.Notify("Saldo insuficiente para realizar a transferência.");
                    return new CreateTransferResult();
                }

                originBankAccount.Balance -= request.Value;
                destinationBankAccount.Balance += request.Value;

                var bankLaunchTransfer = new BankLaunch(request.Value, OperationType.Transfer,
                    originBankAccount.Id, destinationBankAccount.Id);
                _bankLaunchRepository.Add(bankLaunchTransfer);
                break;

            default:
                _notifier.Notify("Tipo de operação inválido.");
                return new CreateTransferResult();
        }

        _bankAccountRepository.Update(originBankAccount);
        _bankAccountRepository.Update(destinationBankAccount);

        await _bankAccountRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateTransferResult();
    }
}
