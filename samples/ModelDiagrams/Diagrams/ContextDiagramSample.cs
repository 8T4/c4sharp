using C4Sharp.Diagrams.Builders;
using C4Sharp.Elements;
using C4Sharp.Elements.Relationships;
using static ModelDiagrams.Structures.People;
using static ModelDiagrams.Structures.Systems;
using static C4Sharp.Elements.Relationships.Position;

namespace ModelDiagrams.Diagrams;

public class ContextDiagramSample : ContextDiagram
{
    protected override string Title => "Component diagram for Internet Banking System";
    
    protected override IEnumerable<Structure> Structures => new Structure[]
    {
        Customer,
        BankingSystem,
        Mainframe,
        MailSystem
    };

    protected override IEnumerable<Relationship> Relationships => new[]
    {
        Customer > BankingSystem,
        Customer < MailSystem | "Sends e-mails to",
        BankingSystem > MailSystem | ("Sends e-mails", "SMTP") | Neighbor,
        BankingSystem > Mainframe
    };
}