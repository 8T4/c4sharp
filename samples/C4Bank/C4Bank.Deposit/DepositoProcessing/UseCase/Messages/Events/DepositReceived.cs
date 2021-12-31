using C4Bank.Deposit.DepositoProcessing.UseCase.Messages.Commands;

namespace C4Bank.Deposit.DepositoProcessing.UseCase.Messages.Events;

public record DepositReceived
{
    public string? DepositCode { get; init; }
    public decimal Amount { get; init; }
    public int AccountId { get; init; }
    public int BankCode { get; init; }
    public int BankAccount { get; init; }
    public int BankAgency { get; init; }
    public DateTime DepositDat { get; init; }
}

internal static class DepositReceivedMapper
{
    public static RegisterDepositCommand Map(DepositReceived @event)
    {
        return new RegisterDepositCommand();
    }
}