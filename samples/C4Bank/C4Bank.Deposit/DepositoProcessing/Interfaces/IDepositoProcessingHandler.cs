using C4Bank.Deposit.DepositoProcessing.UseCase.Messages.Commands;

namespace C4Bank.Deposit.DepositoProcessing.Interfaces;

public interface IDepositoProcessingHandler
{
    Task ExecuteAsync(RegisterDepositCommand command);
}