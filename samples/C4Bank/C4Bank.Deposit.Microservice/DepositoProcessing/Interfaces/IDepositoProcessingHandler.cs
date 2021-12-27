using C4Bank.Deposit.Microservice.DepositoProcessing.UseCase.Messages.Commands;

namespace C4Bank.Deposit.Microservice.DepositoProcessing.Interfaces;

public interface IDepositoProcessingHandler
{
    Task ExecuteAsync(RegisterDepositCommand command);
}