using C4Sharp.Diagrams;
using C4Sharp.Elements;
using C4Sharp.Elements.Relationships;
using C4Sharp.Sample.Diagrams;

namespace C4Sharp.Sample.Structures;

public static class Systems
{
    private static SoftwareSystem? _bankingSystem;

    public static SoftwareSystem BankingSystem => _bankingSystem ??= new SoftwareSystem(
        "BankingSystem",
        "Internet Banking System")
    {
        Description = "Allows customers to view information about their " +
                      "bank accounts, and make payments.",
        Tags = new[] { "services" },
    };

    private static SoftwareSystem? _mainframe;

    public static SoftwareSystem Mainframe => _mainframe ??= new SoftwareSystem(
        "Mainframe",
        "Mainframe Banking System")
    {
        Description = "Stores all of the core banking information about customers, " +
                      "accounts, transactions, etc.",
        Boundary = Boundary.External
    };

    private static SoftwareSystem? _mailSystem;

    public static SoftwareSystem MailSystem => _mailSystem ??= new SoftwareSystem(
        "MailSystem",
        "E-mail system")
    {
        Description = "The internal Microsoft Exchange e-mail system.",
        Boundary = Boundary.External
    };
}
