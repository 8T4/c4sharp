using C4Bank.Deposit.Shared;

namespace C4Bank.Deposit.UseCases.DepositoProcessing.UseCase.Messages.Events;

public record DepositRejected(string[] Reason): IEvent;