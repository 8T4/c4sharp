using C4Bank.Deposit.DepositoProcessing.Interfaces;
using C4Bank.Deposit.DepositoProcessing.UseCase.Messages.Events;

namespace C4Bank.Deposit.DepositoProcessing.Adapters;

public class DepositoProcessingConsumer
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