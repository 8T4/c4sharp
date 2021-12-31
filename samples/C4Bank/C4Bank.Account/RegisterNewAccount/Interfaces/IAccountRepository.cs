using C4Bank.Account.RegisterNewAccount.UseCase.Entities;

namespace C4Bank.Account.RegisterNewAccount.Interfaces;

public interface IAccountRepository
{
    Task Register(UseCase.Entities.Account account);
}