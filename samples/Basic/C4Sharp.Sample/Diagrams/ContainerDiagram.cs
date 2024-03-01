using C4Sharp.Diagrams;
using C4Sharp.Diagrams.Interfaces;
using C4Sharp.Diagrams.Plantuml.Constants;
using C4Sharp.Diagrams.Plantuml.Style;
using C4Sharp.Elements;
using C4Sharp.Elements.Boundaries;
using C4Sharp.Elements.Relationships;
using C4Sharp.Sample.Structures;

namespace C4Sharp.Sample.Diagrams
{
    using static Containers;
    
    public class ContainerDiagram: DiagramBuildRunner
    {
        protected override string Title => "Container diagram for Internet Banking System";
        protected override DiagramType DiagramType => DiagramType.Container;

        protected override IEnumerable<Structure> Structures => new Structure[]
        {
            People.Customer,
            Systems.BankingSystem,
            Systems.MailSystem,
            new SoftwareSystemBoundary("c1", "Internet Banking",
                WebApp,
                Spa,
                MobileApp,
                SqlDatabase,
                RabbitMq,
                BackendApi
            )
        };

        protected override IEnumerable<Relationship> Relationships => new[]
        {
            People.Customer > WebApp | ("Uses", "HTTPS"),
            People.Customer > Spa | ("Uses", "HTTPS"),
            People.Customer > MobileApp | "Uses",

            WebApp > Spa | "Delivers" | Position.Neighbor,
            Spa > BackendApi | ("Uses", "async, JSON/HTTPS"),
            MobileApp > BackendApi | ("Uses", "async, JSON/HTTPS"),
            SqlDatabase < BackendApi | ("Uses", "async, JSON/HTTPS") | Position.Neighbor,
            RabbitMq < BackendApi | ("Uses", "async, JSON"),

            People.Customer < Systems.MailSystem | "Sends e-mails to",
            Systems.MailSystem < BackendApi | ("Sends e-mails using", "sync, SMTP"),
            BackendApi > Systems.BankingSystem | ("Uses", "sync/async, XML/HTTPS") | Position.Neighbor
        };

        protected override IElementTag SetTags()
        {
            return new ElementTag()
                .AddElementTag("services", "#3F6684", shape: Shape.EightSidedShape);
        }
    }
}