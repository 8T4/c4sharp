using C4Sharp.Diagrams;
using C4Sharp.Diagrams.Core;
using C4Sharp.Models;
using C4Sharp.Models.Relationships;
using C4Sharp.Sample.Structures;

namespace C4Sharp.Sample.Diagrams
{
    using static Containers;
    
    public class ContainerDiagramBuildRunner: IDiagramBuildRunner
    {
        public Diagram Build()
        {
            return new ContainerDiagram()
            {
                ShowLegend = true,
                Title = "Container diagram for Internet Banking System",
                Structures = new Structure[]
                {
                    People.Customer,
                    Boundary(),
                    Systems.BankingSystem,
                    Systems.MailSystem,
                },
                Relationships = new[]
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
                }
            };
        }

        private static SoftwareSystemBoundary Boundary()
        {
            return new ("c1", "Internet Banking")
            {
                Containers = new[]
                {
                    WebApp,
                    Spa,
                    MobileApp,
                    SqlDatabase,
                    BackendApi
                }
            };
        }
    }
}