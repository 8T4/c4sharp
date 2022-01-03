using C4Bank.Account.RegisterNewAccount.Interfaces;
using C4Bank.Account.RegisterNewAccount.UseCase.Messages.Events;

namespace C4Bank.Account.RegisterNewAccount.Adapters;

public class RegisterNewAccountConsumer
{
    private readonly IRegisterNewAccountHandler _handler;

    public RegisterNewAccountConsumer(IRegisterNewAccountHandler handler)
    {
        _handler = handler;
    }
    
    public async Task Consume(AccountRequestReceived @event)
    {
        var command = DepositReceivedMapper.Map(@event);
        await _handler.ExecuteAsync(command);
    }
}