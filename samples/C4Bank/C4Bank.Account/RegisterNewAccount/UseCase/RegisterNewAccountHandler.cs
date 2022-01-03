using C4Bank.Account.RegisterNewAccount.Interfaces;
using C4Bank.Account.RegisterNewAccount.UseCase.Entities;
using C4Bank.Account.RegisterNewAccount.UseCase.Messages.Commands;
using C4Bank.Account.RegisterNewAccount.UseCase.Messages.Events;

namespace C4Bank.Account.RegisterNewAccount.UseCase;

public class RegisterNewAccountHandler : IRegisterNewAccountHandler
{
    private readonly IAccountRepository _repository;
    private readonly IRegisterNewAccountProducer _producer;

    public RegisterNewAccountHandler(IAccountRepository repository, IRegisterNewAccountProducer producer)
    {
        _repository = repository;
        _producer = producer;
    }
    
    public async Task ExecuteAsync(RegisterAccountCommand command)
    {
        var deposit = RegisterNewAccountMapper.Map<Entities.Account>(command);
        await _repository.Register(deposit);

        var @event = RegisterNewAccountMapper.Map<RegisteredAccount>(deposit);
        await _producer.Compleat(@event);
    }
}