using C4Bank.Deposit.UseCases.SynchronizeNewAccount.Interfaces;
using C4Bank.Deposit.UseCases.SynchronizeNewAccount.UseCase;
using C4Bank.Deposit.UseCases.SynchronizeNewAccount.UseCase.Messages.Commands;
using C4Bank.Deposit.UseCases.SynchronizeNewAccount.UseCase.Messages.Events;

namespace C4Bank.Deposit.UseCases.SynchronizeNewAccount.Adapters;

public class SynchronizeNewAccountConsumer
{
    private readonly ISynchronizeNewAccountHandler _handler;

    public SynchronizeNewAccountConsumer(ISynchronizeNewAccountHandler handler)
    {
        _handler = handler;
    }
    
    public async Task Consume(RegisteredAccount @event)
    {
        var command = SynchronizeNewAccountMapper.Map<SynchronizedAccountCommand>(@event);
        await _handler.ExecuteAsync(command);
    }
}