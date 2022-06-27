using C4Bank.Deposit.Shared;
using C4Bank.Deposit.UseCases.DepositProcessing.UseCase.Messages.Commands;

namespace C4Bank.Deposit.UseCases.DepositProcessing.Interfaces;

public interface IDepositProcessingHandler: IHandler
{
    Task ExecuteAsync(RegisterDepositCommand command);
}