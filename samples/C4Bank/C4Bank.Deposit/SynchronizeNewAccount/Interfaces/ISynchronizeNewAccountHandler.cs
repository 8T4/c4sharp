using C4Bank.Deposit.SynchronizeNewAccount.UseCase.Messages.Commands;

namespace C4Bank.Deposit.SynchronizeNewAccount.Interfaces;

public interface ISynchronizeNewAccountHandler
{
    Task ExecuteAsync(SynchronizedAccountCommand command);
}