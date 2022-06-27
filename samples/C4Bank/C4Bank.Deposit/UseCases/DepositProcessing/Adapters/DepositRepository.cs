using C4Bank.Deposit.UseCases.DepositoProcessing.Interfaces;
using C4Bank.Deposit.UseCases.DepositProcessing.Interfaces;
using C4Bank.Deposit.UseCases.DepositProcessing.UseCase.Entities;

namespace C4Bank.Deposit.UseCases.DepositProcessing.Adapters;

public class DepositRepository: IDepositRepository
{
    public Task Register(AccountDeposit accountDeposit)
    {
        throw new NotImplementedException();
    }
}