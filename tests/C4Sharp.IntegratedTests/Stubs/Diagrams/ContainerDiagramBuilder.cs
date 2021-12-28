using C4Sharp.Diagrams.Core;
using C4Sharp.IntegratedTests.Stubs.Models;
using C4Sharp.Models;
using C4Sharp.Models.Relationships;
using static C4Sharp.IntegratedTests.Stubs.Models.Containers;

namespace C4Sharp.IntegratedTests.Stubs.Diagrams;
public static class ContainerDiagramBuilder
{
    public static ContainerDiagram Build() => new()
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

    private static SoftwareSystemBoundary Boundary() => new("c1", "Internet Banking")
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
