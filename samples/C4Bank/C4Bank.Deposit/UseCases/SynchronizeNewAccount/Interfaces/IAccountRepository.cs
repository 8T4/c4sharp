namespace C4Bank.Deposit.UseCases.SynchronizeNewAccount.Interfaces;

public interface IAccountRepository
{
    Task Register(UseCase.Entities.Account account);
}