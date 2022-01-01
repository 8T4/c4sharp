using C4Bank.Deposit.UseCases.DepositoProcessing.Interfaces;
using C4Bank.Deposit.UseCases.DepositoProcessing.UseCase.Entities;
using C4Bank.Deposit.UseCases.DepositoProcessing.UseCase.Messages.Commands;
using C4Bank.Deposit.UseCases.DepositoProcessing.UseCase.Messages.Events;

namespace C4Bank.Deposit.UseCases.DepositoProcessing.UseCase;

public class DepositoProcessingHandler : IDepositoProcessingHandler
{
    private readonly IDepositRepository _repository;
    private readonly IDepositoProcessingProducer _producer;

    public DepositoProcessingHandler(IDepositRepository repository, IDepositoProcessingProducer producer)
    {
        _repository = repository;
        _producer = producer;
    }
    
    public async Task ExecuteAsync(RegisterDepositCommand command)
    {
        var deposit = DepositProcessingMapper.Map<AccountDeposit>(command);
        await _repository.Register(deposit);

        var @event = DepositProcessingMapper.Map<RegisteredDeposit>(deposit);
        await _producer.Compleat(@event);
    }
}