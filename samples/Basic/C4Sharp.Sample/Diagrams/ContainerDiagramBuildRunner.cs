using C4Sharp.Diagrams;
using C4Sharp.Models;
using C4Sharp.Models.Relationships;
using C4Sharp.Sample.Structures;

namespace C4Sharp.Sample.Diagrams
{
    using static Containers;
    
    public class ContainerDiagramBuildRunner: DiagramBuildRunner
    {
        public override string Title => "Container diagram for Internet Banking System";
        public override DiagramType DiagramType => DiagramType.Container;

        protected override IEnumerable<Structure> Structures() => new Structure[]
        {
            People.Customer,
            Systems.BankingSystem,
            Systems.MailSystem,
            new SoftwareSystemBoundary("c1", "Internet Banking",
                WebApp,
                Spa,
                MobileApp,
                SqlDatabase,
                BackendApi
            )
        };

        protected override IEnumerable<Relationship> Relationships() => new[]
        {
            (People.Customer > WebApp)["Uses", "HTTPS"],
            (People.Customer > Spa)["Uses", "HTTPS"],
            (People.Customer > MobileApp)["Uses"],

            (WebApp > Spa)["Delivers"][Position.Neighbor],
            (Spa > BackendApi)["Uses", "async, JSON/HTTPS"],
            (MobileApp > BackendApi)["Uses", "async, JSON/HTTPS"],
            (SqlDatabase < BackendApi)["Uses", "async, JSON/HTTPS"][Position.Neighbor],

            (People.Customer < Systems.MailSystem)["Sends e-mails to"],
            (Systems.MailSystem < BackendApi)["Sends e-mails using", "sync, SMTP"],
            (BackendApi > Systems.BankingSystem)["Uses", "sync/async, XML/HTTPS"][Position.Neighbor]
        };
    }
}