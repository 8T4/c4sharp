using C4Sharp.Diagrams.Core;
using C4Sharp.Models;
using static C4Sharp.IntegratedTests.Stubs.Models.People;
using static C4Sharp.IntegratedTests.Stubs.Models.Systems;
using static C4Sharp.Models.Relationships.Position;

namespace C4Sharp.IntegratedTests.Stubs.Diagrams;
public static class ContextDiagramBuilder
{
    public static ContextDiagram Build() => new()
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
            Customer > BankingSystem,
            (Customer < MailSystem)["Sends e-mails to"],
            (BankingSystem > MailSystem)["Sends e-mails", "SMTP"][Neighbor],
            BankingSystem > Mainframe,
        }
    };
}
