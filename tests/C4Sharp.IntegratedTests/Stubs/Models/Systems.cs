using C4Sharp.Models;
using C4Sharp.Models.Relationships;

namespace C4Sharp.IntegratedTests.Stubs.Models;

public static class Systems
{
    private static SoftwareSystem _bankingSystem;

    public static SoftwareSystem BankingSystem => _bankingSystem ??= new SoftwareSystem(
        "BankingSystem",
        "Internet Banking System")
    {
        Description = "Allows customers to view information about their bank accounts, and make payments."
    };

    private static SoftwareSystem _mainframe;

    public static SoftwareSystem Mainframe => _mainframe ??= new SoftwareSystem(
        "Mainframe",
        "Mainframe Banking System")
    {
        Description = "Stores all of the core banking information about customers, accounts, transactions, etc.",
        Boundary = Boundary.External
    };

    private static SoftwareSystem _mailSystem;

    public static SoftwareSystem MailSystem => _mailSystem ??= new SoftwareSystem(
        "MailSystem",
        "E-mail system")
    {
        Description = "The internal Microsoft Exchange e-mail system.",
        Boundary = Boundary.External
    };
}
