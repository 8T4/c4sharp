using C4Bank.Deposit.UseCases.SynchronizeNewAccount.Interfaces;

namespace C4Bank.Deposit.UseCases.SynchronizeNewAccount.Adapters;

public class AccountRepository: IAccountRepository
{
    public Task Register(UseCase.Entities.Account account)
    {
        throw new NotImplementedException();
    }
}