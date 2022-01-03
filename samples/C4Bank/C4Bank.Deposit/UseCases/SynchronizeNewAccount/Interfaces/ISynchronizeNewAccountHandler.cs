using C4Bank.Deposit.UseCases.SynchronizeNewAccount.UseCase.Messages.Commands;

namespace C4Bank.Deposit.UseCases.SynchronizeNewAccount.Interfaces;

public interface ISynchronizeNewAccountHandler
{
    Task ExecuteAsync(SynchronizedAccountCommand command);
}