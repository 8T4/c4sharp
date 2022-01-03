using C4Bank.Deposit.UseCases.DepositoProcessing.UseCase.Entities;

namespace C4Bank.Deposit.UseCases.DepositoProcessing.Interfaces;

public interface IDepositRepository
{
    Task Register(AccountDeposit accountDeposit);
}