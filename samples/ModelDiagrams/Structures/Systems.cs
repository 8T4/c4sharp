using C4Sharp.Elements;
using C4Sharp.Elements.Relationships;

namespace ModelDiagrams.Structures;

public static class Systems
{
    public static SoftwareSystem BankingSystem => new(
        alias: "BankingSystem",
        label: "Internet Banking System",
        description: "Allows customers to view information about their bank accounts, and make payments."
    );

    public static SoftwareSystem Mainframe => new(
        alias: "Mainframe",
        label: "Mainframe Banking System",
        description: "Stores all of the core banking information about customers, accounts, transactions, etc.",
        boundary: Boundary.External
    );

    public static SoftwareSystem MailSystem => new(
        alias: "MailSystem",
        label: "E-mail system",
        description: "The internal Microsoft Exchange e-mail system.",
        boundary: Boundary.External
    );
}