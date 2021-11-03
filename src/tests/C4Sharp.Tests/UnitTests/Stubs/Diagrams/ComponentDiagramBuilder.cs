using C4Sharp.Diagrams;
using C4Sharp.Diagrams.Core;
using C4Sharp.Models;
using C4Sharp.Tests.Stubs.Models;

namespace C4Sharp.Tests.Stubs.Diagrams
{
    using static Systems;    
    using static Components;    
    using static Containers;    
    
    public static class ComponentDiagramBuilder
    {
        public static ComponentDiagram Build()
        {
            return new()
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
        }

        private static ContainerBoundary Boundary()
        {
            return new ("c1", "API Application")
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
    }
}