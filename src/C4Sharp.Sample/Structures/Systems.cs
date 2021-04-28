using C4Sharp.Models;
using C4Sharp.Models.Relationships;

namespace C4Sharp.Sample.Structures
{
    public static class Systems
    {
        private static SoftwareSystem _bankingSystem;
        public static SoftwareSystem BankingSystem => _bankingSystem ??= new SoftwareSystem(
            alias: "BankingSystem",
            label: "Internet Banking System",
            description: "Allows customers to view information about their bank accounts, and make payments."
        );

        private static SoftwareSystem _mainframe;
        public static SoftwareSystem Mainframe => _mainframe ??= new SoftwareSystem(
            alias: "Mainframe",
            label: "Mainframe Banking System",
            description: "Stores all of the core banking information about customers, accounts, transactions, etc.",
            boundary: Boundary.External
        );

        private static SoftwareSystem _mailSystem;
        public static SoftwareSystem MailSystem => _mailSystem ??= new SoftwareSystem(
            alias: "MailSystem",
            label: "E-mail system",
            description: "The internal Microsoft Exchange e-mail system.",
            boundary: Boundary.External
        );
    }
}