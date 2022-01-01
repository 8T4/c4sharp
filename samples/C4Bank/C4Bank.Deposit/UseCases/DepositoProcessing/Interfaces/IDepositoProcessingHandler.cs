using C4Bank.Deposit.Shared;
using C4Bank.Deposit.UseCases.DepositoProcessing.UseCase.Messages.Commands;

namespace C4Bank.Deposit.UseCases.DepositoProcessing.Interfaces;

public interface IDepositoProcessingHandler: IHandler
{
    Task ExecuteAsync(RegisterDepositCommand command);
}