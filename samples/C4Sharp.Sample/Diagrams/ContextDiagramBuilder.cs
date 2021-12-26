using C4Sharp.Diagrams.Core;
using C4Sharp.Models;
using static C4Sharp.Models.Relationships.Position;
using static C4Sharp.Sample.Structures.People;
using static C4Sharp.Sample.Structures.Systems;

namespace C4Sharp.Sample.Diagrams;
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
            (Customer > BankingSystem).AddTags("error"),
            (Customer < MailSystem)["Sends e-mails to"],
            (BankingSystem > MailSystem)["Sends e-mails", "SMTP"][Neighbor],
            BankingSystem > Mainframe,
        }
    };
}
