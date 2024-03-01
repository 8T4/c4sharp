using C4Sharp.Diagrams;
using C4Sharp.Diagrams.Interfaces;
using C4Sharp.Diagrams.Plantuml.Constants;
using C4Sharp.Diagrams.Plantuml.Style;
using C4Sharp.Elements;
using C4Sharp.Elements.Relationships;
using C4Sharp.Sample.Structures;

namespace C4Sharp.Sample.Diagrams
{
    using static Position;
    using static People;
    using static Systems;

    public class ContextDiagram : DiagramBuildRunner
    {
        protected override string Title => "System Context diagram for Internet Banking System";
        protected override DiagramType DiagramType => DiagramType.Context;

        protected override IEnumerable<Structure> Structures => new Structure[]
        {
            Customer,
            BankingSystem,
            Mainframe,
            MailSystem
        };

        protected override IEnumerable<Relationship> Relationships => new[]
        {
            (Customer > BankingSystem).AddTags("error"),
            Customer < MailSystem | "Sends e-mails to",
            BankingSystem > MailSystem | ("Sends e-mails", "SMTP") | Neighbor,
            BankingSystem > Mainframe
        };

        protected override IElementStyle SetStyle()
        {
            return new ElementStyle()
                .UpdateElementStyle(ElementName.ExternalPerson, "#7f3b08", "#7f3b08")
                .UpdateElementStyle(ElementName.Person, "#55ACEE", "#55ACEE")
                .UpdateElementStyle(ElementName.ExternalSystem, "#3F6684", shape: Shape.RoundedBoxShape);
        }

        protected override IRelationshipTag? SetRelTags()
        {
            return new RelationshipTag()
                .AddRelTag("error", "red", "red", LineStyle.DashedLine);
        }
    }
}