using C4Bank.Deposit.UseCases.DepositoProcessing.Interfaces;
using C4Bank.Deposit.UseCases.DepositoProcessing.UseCase.Entities;

namespace C4Bank.Deposit.UseCases.DepositoProcessing.Adapters;

public class DepositRepository: IDepositRepository
{
    public Task Register(AccountDeposit accountDeposit)
    {
        throw new NotImplementedException();
    }
}