using C4Bank.Deposit.Shared;
using C4Bank.Deposit.UseCases.DepositoProcessing.Interfaces;
using C4Bank.Deposit.UseCases.DepositoProcessing.UseCase.Messages.Events;

namespace C4Bank.Deposit.UseCases.DepositoProcessing.Adapters;

public class DepositoProcessingConsumer: IConsumer
{
    private readonly IDepositoProcessingHandler _handler;

    public DepositoProcessingConsumer(IDepositoProcessingHandler handler)
    {
        _handler = handler;
    }
    
    public async Task Consume(DepositReceived @event)
    {
        var command = DepositReceivedMapper.Map(@event);
        await _handler.ExecuteAsync(command);
    }
}