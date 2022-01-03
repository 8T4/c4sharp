using C4Bank.Deposit.Shared;
using C4Bank.Deposit.UseCases.DepositoProcessing.UseCase.Messages.Commands;

namespace C4Bank.Deposit.UseCases.DepositoProcessing.UseCase.Messages.Events;

public record DepositReceived: IEvent
{
    public string? DepositCode { get; init; }
    public decimal Amount { get; init; }
    public int AccountId { get; init; }
    public int BankCode { get; init; }
    public int BankAccount { get; init; }
    public int BankAgency { get; init; }
    public DateTime DepositDat { get; init; }
}