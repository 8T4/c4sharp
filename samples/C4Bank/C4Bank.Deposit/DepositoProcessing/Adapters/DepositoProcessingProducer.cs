using C4Bank.Deposit.DepositoProcessing.Interfaces;
using C4Bank.Deposit.DepositoProcessing.UseCase.Messages.Events;

namespace C4Bank.Deposit.DepositoProcessing.Adapters;

public class DepositoProcessingProducer: IDepositoProcessingProducer
{
    public Task Compleat(RegisteredDeposit @event)
    {
        return Task.CompletedTask;
    }

    public Task Reject(DepositRejected @event)
    {
        return Task.CompletedTask;
    }
}