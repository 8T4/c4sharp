using C4Bank.Deposit.Microservice.DepositoProcessing.Interfaces;
using C4Bank.Deposit.Microservice.DepositoProcessing.UseCase.Messages.Events;

namespace C4Bank.Deposit.Microservice.DepositoProcessing.Adapters;

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