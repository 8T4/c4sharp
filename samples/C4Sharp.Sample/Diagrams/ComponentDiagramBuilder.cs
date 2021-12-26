using C4Sharp.Diagrams;
using C4Sharp.Diagrams.Core;
using C4Sharp.Models;
using static C4Sharp.Sample.Structures.Components;
using static C4Sharp.Sample.Structures.Containers;
using static C4Sharp.Sample.Structures.Systems;

namespace C4Sharp.Sample.Diagrams;

public static class ComponentDiagramBuilder
{
    public static ComponentDiagram Build() => new()
    {
        Title = "Internet Banking System API Application",
        FlowVisualization = DiagramLayout.LeftRight,
        LayoutAsSketch = true,
        Structures = new Structure[]
        {
            Spa,
            MobileApp,
            SqlDatabase,
            Mainframe,
            Boundary(),
        },
        Relationships = new[]
        {
            (Spa > Sign)["Uses", "JSON/HTTPS"],
            (Spa > Accounts)["Uses", "JSON/HTTPS"],
            (MobileApp > Sign)["Uses", "JSON/HTTPS"],
            (MobileApp > Accounts)["Uses", "JSON/HTTPS"],
        }
    };

    private static ContainerBoundary Boundary() => new("c1", "API Application")
    {
        Components = new[]
        {
            Sign,
            Accounts,
            Security,
            MainframeFacade
        },
        Relationships = new[]
        {
            Sign > Security,
            Accounts > MainframeFacade,
            (Security > SqlDatabase)["Read & write to", "JDBC"],
            (MainframeFacade > Mainframe)["Uses", "XML/HTTPS"]
        }
    };
}
