using C4Sharp.Diagrams.Core;
using C4Sharp.Models;
using C4Sharp.Models.Relationships;
using C4Sharp.Sample.Structures;

namespace C4Sharp.Sample.Diagrams
{
    using static Position;
    using static People;
    using static Systems;

    public static class ContextDiagramBuilder
    {
        public static ContextDiagram Build()
        {
            return new ContextDiagram()
            {
                Title = "System Context diagram for Internet Banking System",
                Structures = new Structure[]
                {
                    Customer,
                    BankingSystem,
                    Mainframe,
                    MailSystem
                },
                Relationships = new[]
                {
                    (Customer > BankingSystem).AddTags("error"),
                    (Customer < MailSystem)["Sends e-mails to"],
                    (BankingSystem > MailSystem)["Sends e-mails", "SMTP"][Neighbor],
                    BankingSystem > Mainframe,
                }
            };
        }
    }
}