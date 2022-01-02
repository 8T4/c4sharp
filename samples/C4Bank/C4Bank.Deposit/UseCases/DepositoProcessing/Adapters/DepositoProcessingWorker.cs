using System.ComponentModel;
using C4Bank.Deposit.Shared;
using C4Bank.Deposit.UseCases.DepositoProcessing.Interfaces;
using C4Bank.Deposit.UseCases.DepositoProcessing.UseCase;
using C4Bank.Deposit.UseCases.DepositoProcessing.UseCase.Messages.Commands;
using C4Bank.Deposit.UseCases.DepositoProcessing.UseCase.Messages.Events;

namespace C4Bank.Deposit.UseCases.DepositoProcessing.Adapters;

[Description("Deposit Worker API")]
public class DepositoProcessingWorker: IWorker
{
    private readonly IDepositoProcessingHandler _handler;

    public DepositoProcessingWorker(IDepositoProcessingHandler handler)
    {
        _handler = handler;
    }
    
    public async Task Consume(DepositReceived @event)
    {
        var command = DepositProcessingMapper.Map<RegisterDepositCommand>(@event);
        await _handler.ExecuteAsync(command);
    }
}