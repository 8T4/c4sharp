using C4Bank.Deposit.DepositoProcessing.UseCase.Entities;

namespace C4Bank.Deposit.DepositoProcessing.Interfaces;

public interface IDepositRepository
{
    Task Register(AccountDeposit accountDeposit);
}