using C4Bank.Deposit.UseCases.SynchronizeNewAccount.Interfaces;
using C4Bank.Deposit.UseCases.SynchronizeNewAccount.UseCase.Messages.Events;

namespace C4Bank.Deposit.UseCases.SynchronizeNewAccount.Adapters;

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