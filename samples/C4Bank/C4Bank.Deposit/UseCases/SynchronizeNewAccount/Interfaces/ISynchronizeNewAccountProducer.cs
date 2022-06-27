using C4Bank.Deposit.UseCases.SynchronizeNewAccount.UseCase.Messages.Events;

namespace C4Bank.Deposit.UseCases.SynchronizeNewAccount.Interfaces;

public interface ISynchronizeNewAccountProducer
{
    Task Complete(AccountSynchronized @event);
    Task Reject(AccountSynchronizationRejected @event);
}