using C4Bank.Deposit.DepositoProcessing.UseCase.Messages.Events;

namespace C4Bank.Deposit.DepositoProcessing.Interfaces;

public interface IDepositoProcessingProducer
{
    Task Compleat(RegisteredDeposit @event);
    Task Reject(DepositRejected @event);
}