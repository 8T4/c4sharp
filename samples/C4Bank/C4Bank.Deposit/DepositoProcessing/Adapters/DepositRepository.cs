using C4Bank.Deposit.DepositoProcessing.Interfaces;
using C4Bank.Deposit.DepositoProcessing.UseCase.Entities;

namespace C4Bank.Deposit.DepositoProcessing.Adapters;

public class DepositRepository: IDepositRepository
{
    public Task Register(AccountDeposit accountDeposit)
    {
        throw new NotImplementedException();
    }
}