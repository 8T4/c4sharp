using C4Bank.Account.RegisterNewAccount.Interfaces;
using C4Bank.Account.RegisterNewAccount.UseCase.Messages.Commands;
using C4Bank.Account.RegisterNewAccount.UseCase.Messages.Events;

namespace C4Bank.Account.RegisterNewAccount.UseCase;

/// <summary>
/// This class represents the use of Decorator pattern
/// <see href="https://refactoring.guru/design-patterns/decorator"/>
/// </summary>
public class RegisterNewAccountValidation: IRegisterNewAccountHandler
{
    private readonly IRegisterNewAccountHandler _handler;
    private readonly IRegisterNewAccountProducer _producer;

    public RegisterNewAccountValidation(IRegisterNewAccountHandler handler, IRegisterNewAccountProducer producer)
    {
        _handler = handler;
        _producer = producer;
    }
    
    public async Task ExecuteAsync(RegisterAccountCommand command)
    {
        var (success, messages) = Validate(command);

        if (!success)
        {
            var @event = RegisterNewAccountMapper.Map<AccountRejected>(command);
            await _producer.Reject(@event with { Reason = messages });
        }

        await _handler.ExecuteAsync(command);
    }

    private (bool success, string[] messages) Validate(RegisterAccountCommand command)
    {
        return (true, Array.Empty<string>());
    }
}