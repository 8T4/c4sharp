using C4Bank.Deposit.SynchronizeNewAccount.Interfaces;
using C4Bank.Deposit.SynchronizeNewAccount.UseCase.Messages.Events;

namespace C4Bank.Deposit.SynchronizeNewAccount.Adapters;

public class SynchronizeNewAccountProducer: ISynchronizeNewAccountProducer
{
    public Task Compleat(AccountSynchronized @event)
    {
        return Task.CompletedTask;
    }

    public Task Reject(AccountSynchronizationRejected @event)
    {
        return Task.CompletedTask;
    }
}