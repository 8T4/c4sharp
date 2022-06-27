using C4Bank.Deposit.Shared;
using C4Bank.Deposit.UseCases.DepositProcessing.UseCase.Messages.Events;

namespace C4Bank.Deposit.UseCases.DepositoProcessing.Interfaces;

public interface IDepositoProcessingProducer : IProducer
{
    Task Complete(RegisteredDeposit @event);
    Task Reject(DepositRejected @event);
}