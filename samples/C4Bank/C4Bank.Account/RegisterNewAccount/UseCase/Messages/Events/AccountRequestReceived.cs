using C4Bank.Account.RegisterNewAccount.UseCase.Messages.Commands;

namespace C4Bank.Account.RegisterNewAccount.UseCase.Messages.Events;

public record AccountRequestReceived();

internal static class DepositReceivedMapper
{
    public static RegisterAccountCommand Map(AccountRequestReceived @event)
    {
        return new RegisterAccountCommand();
    }
}