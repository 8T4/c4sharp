using C4Bank.Deposit.SynchronizeNewAccount.UseCase.Messages.Events;

namespace C4Bank.Deposit.SynchronizeNewAccount.Interfaces;

public interface ISynchronizeNewAccountProducer
{
    Task Compleat(AccountSynchronized @event);
    Task Reject(AccountSynchronizationRejected @event);
}