using C4Bank.Deposit.UseCases.SynchronizeNewAccount.Interfaces;
using C4Bank.Deposit.UseCases.SynchronizeNewAccount.UseCase.Messages.Commands;
using C4Bank.Deposit.UseCases.SynchronizeNewAccount.UseCase.Messages.Events;

namespace C4Bank.Deposit.UseCases.SynchronizeNewAccount.UseCase;

public class SynchronizeNewAccountHandler : ISynchronizeNewAccountHandler
{
    private readonly IAccountRepository _repository;
    private readonly ISynchronizeNewAccountProducer _producer;

    public SynchronizeNewAccountHandler(IAccountRepository repository, ISynchronizeNewAccountProducer producer)
    {
        _repository = repository;
        _producer = producer;
    }
    
    public async Task ExecuteAsync(SynchronizedAccountCommand command)
    {
        var deposit = SynchronizeNewAccountMapper.Map<Entities.Account>(command);
        await _repository.Register(deposit);

        var @event = SynchronizeNewAccountMapper.Map<AccountSynchronized>(deposit);
        await _producer.Compleat(@event);
    }
}