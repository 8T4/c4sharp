using C4Bank.Deposit.UseCases.SynchronizeNewAccount.Interfaces;
using C4Bank.Deposit.UseCases.SynchronizeNewAccount.UseCase.Messages.Commands;
using C4Bank.Deposit.UseCases.SynchronizeNewAccount.UseCase.Messages.Events;

namespace C4Bank.Deposit.UseCases.SynchronizeNewAccount.UseCase;

/// <summary>
/// This class represents the use of Decorator pattern
/// <see href="https://refactoring.guru/design-patterns/decorator"/>
/// </summary>
public class SynchronizeNewAccountValidation: ISynchronizeNewAccountHandler
{
    private readonly ISynchronizeNewAccountHandler _handler;
    private readonly ISynchronizeNewAccountProducer _producer;

    public SynchronizeNewAccountValidation(ISynchronizeNewAccountHandler handler, ISynchronizeNewAccountProducer producer)
    {
        _handler = handler;
        _producer = producer;
    }
    
    public async Task ExecuteAsync(SynchronizedAccountCommand command)
    {
        var (success, messages) = Validate(command);

        if (!success)
        {
            var @event = SynchronizeNewAccountMapper.Map<AccountSynchronizationRejected>(command);
            await _producer.Reject(@event with { Reason = messages });
        }

        await _handler.ExecuteAsync(command);
    }

    private (bool success, string[] messages) Validate(SynchronizedAccountCommand command)
    {
        return (true, Array.Empty<string>());
    }
}