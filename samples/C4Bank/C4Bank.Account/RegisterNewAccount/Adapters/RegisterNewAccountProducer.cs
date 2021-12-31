using C4Bank.Account.RegisterNewAccount.Interfaces;
using C4Bank.Account.RegisterNewAccount.UseCase.Messages.Events;

namespace C4Bank.Account.RegisterNewAccount.Adapters;

public class RegisterNewAccountProducer: IRegisterNewAccountProducer
{
    public Task Compleat(RegisteredAccount @event)
    {
        return Task.CompletedTask;
    }

    public Task Reject(AccountRejected @event)
    {
        return Task.CompletedTask;
    }
}