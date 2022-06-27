using C4Bank.Deposit.UseCases.DepositProcessing.UseCase.Entities;

namespace C4Bank.Deposit.UseCases.DepositProcessing.Interfaces;

public interface IDepositRepository
{
    Task Register(AccountDeposit accountDeposit);
}