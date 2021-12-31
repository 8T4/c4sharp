namespace C4Bank.Deposit.SynchronizeNewAccount.Interfaces;

public interface IAccountRepository
{
    Task Register(UseCase.Entities.Account account);
}