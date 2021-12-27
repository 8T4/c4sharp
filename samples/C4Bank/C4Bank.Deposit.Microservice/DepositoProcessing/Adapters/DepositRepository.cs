using C4Bank.Deposit.Microservice.DepositoProcessing.Interfaces;
using C4Bank.Deposit.Microservice.DepositoProcessing.UseCase.Entities;

namespace C4Bank.Deposit.Microservice.DepositoProcessing.Adapters;

public class DepositRepository: IDepositRepository
{
    public Task Register(AccountDeposit accountDeposit)
    {
        throw new NotImplementedException();
    }
}