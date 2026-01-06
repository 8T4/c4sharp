using C4Sharp.Diagrams.Builders;
using C4Sharp.Elements;
using C4Sharp.Elements.Relationships;
using ModelDiagrams.Structures;

namespace ModelDiagrams.Diagrams;

using static People;
using static Systems;
using static C4Sharp.Elements.Relationships.Position;

public class EnterpriseDiagramSample : ContextDiagram
{
    protected override string Title => "System Enterprise diagram for Internet Banking System";

    protected override IEnumerable<Structure> Structures =>
    [
       Customer,
       Bound("enterprise.boundary", "Domain A",
           BankingSystem,
           Bound("enterprise.boundary.1", "Domain Internal Users", InternalCustomer),
           Bound("enterprise.boundary.2", "Domain Managers", Manager)
       )//,
       //Mainframe, // Ensure 'Mainframe' is defined in the 'Systems' static class
       //MailSystem // Ensure 'MailSystem' is included in the Structures
    ];

    protected override IEnumerable<Relationship> Relationships => new[]
    {
        Customer > BankingSystem,
        InternalCustomer > BankingSystem,
        Manager > BankingSystem,
        Customer < MailSystem | "Sends e-mails to",
        BankingSystem > MailSystem | ("Sends e-mails", "SMTP") | Neighbor,
        BankingSystem > Mainframe,
    };
}
