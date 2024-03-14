using C4Sharp.Elements;
using C4Sharp.Elements.Relationships;

namespace ModelDiagrams.Structures;

public static class Systems
{
    public static SoftwareSystem BankingSystem =>
        new("BankingSystem", "Internet Banking System",
            "Allows customers to view information about their bank accounts, and make payments.");

    public static SoftwareSystem Mainframe => 
        new("Mainframe", "Mainframe Banking System",
        "Stores all of the core banking information about customers, accounts, transactions, etc.", Boundary.External);

    public static SoftwareSystem MailSystem => 
        new ("MailSystem", "E-mail system", "The internal Microsoft Exchange e-mail system.", Boundary.External);
}
