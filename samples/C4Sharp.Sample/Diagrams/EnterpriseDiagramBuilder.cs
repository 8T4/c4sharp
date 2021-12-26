using C4Sharp.Diagrams.Core;
using C4Sharp.Models;
using static C4Sharp.Models.Relationships.Position;
using static C4Sharp.Sample.Structures.People;
using static C4Sharp.Sample.Structures.Systems;

namespace C4Sharp.Sample.Diagrams;
public class EnterpriseDiagramBuilder
{
    public static ContextDiagram Build() => new()
    {
        Title = "System Enterprise diagram for Internet Banking System",
        Structures = new Structure[]
        {
            Customer,
            new EnterpriseBoundary("eboundary", "Domain A")
            {
                Structures = new Structure []
                {
                    BankingSystem,
                    new EnterpriseBoundary("eboundary1", "Domain Internal Users")
                    {
                        Structures = new Structure []
                        {
                            InternalCustomer,
                        }
                    },
                    new EnterpriseBoundary("eboundary2", "Domain Managers")
                    {
                        Structures = new Structure []
                        {
                            Manager,
                        }
                    },
                }
            },
            Mainframe,
            MailSystem
        },
        Relationships = new[]
        {
            Customer > BankingSystem,
            InternalCustomer > BankingSystem,
            Manager > BankingSystem,
            (Customer < MailSystem)["Sends e-mails to"],
            (BankingSystem > MailSystem)["Sends e-mails", "SMTP"][Neighbor],
            BankingSystem > Mainframe,
        }
    };
}
