using C4Bank.Deposit.Microservice.DepositoProcessing.UseCase.Entities;

namespace C4Bank.Deposit.Microservice.DepositoProcessing.Interfaces;

public interface IDepositRepository
{
    Task Register(AccountDeposit accountDeposit);
}