using C4Bank.Account.RegisterNewAccount.Interfaces;
using C4Bank.Account.RegisterNewAccount.UseCase.Entities;

namespace C4Bank.Account.RegisterNewAccount.Adapters;

public class AccountRepository: IAccountRepository
{
    public Task Register(UseCase.Entities.Account account)
    {
        throw new NotImplementedException();
    }
}