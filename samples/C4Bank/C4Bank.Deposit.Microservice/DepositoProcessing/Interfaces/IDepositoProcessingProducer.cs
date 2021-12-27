using C4Bank.Deposit.Microservice.DepositoProcessing.UseCase.Messages.Events;

namespace C4Bank.Deposit.Microservice.DepositoProcessing.Interfaces;

public interface IDepositoProcessingProducer
{
    Task Compleat(RegisteredDeposit @event);
    Task Reject(DepositRejected @event);
}