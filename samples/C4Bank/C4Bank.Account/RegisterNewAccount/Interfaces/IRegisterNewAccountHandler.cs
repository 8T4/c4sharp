using C4Bank.Account.RegisterNewAccount.UseCase.Messages.Commands;

namespace C4Bank.Account.RegisterNewAccount.Interfaces;

public interface IRegisterNewAccountHandler
{
    Task ExecuteAsync(RegisterAccountCommand command);
}