using C4Bank.Deposit.UseCases.DepositoProcessing.UseCase.Messages.Commands;

namespace C4Bank.Deposit.UseCases.DepositoProcessing.Interfaces;

public interface IDepositoProcessingHandler
{
    Task ExecuteAsync(RegisterDepositCommand command);
}