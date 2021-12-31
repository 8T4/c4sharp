using C4Bank.Account.RegisterNewAccount.UseCase.Messages.Events;

namespace C4Bank.Account.RegisterNewAccount.Interfaces;

public interface IRegisterNewAccountProducer
{
    Task Compleat(RegisteredAccount @event);
    Task Reject(AccountRejected @event);
}