using C4Bank.Deposit.UseCases.DepositoProcessing.Interfaces;
using C4Bank.Deposit.UseCases.DepositProcessing.UseCase.Messages.Events;

namespace C4Bank.Deposit.UseCases.DepositProcessing.Adapters;

public class DepositProcessingProducer: IDepositoProcessingProducer
{
    public Task Complete(RegisteredDeposit @event)
    {
        return Task.CompletedTask;
    }

    public Task Reject(DepositRejected @event)
    {
        return Task.CompletedTask;
    }
}