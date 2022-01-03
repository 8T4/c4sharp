using C4Sharp.Diagrams;
using C4Sharp.Diagrams.Core;
using C4Sharp.Models;
using C4Sharp.Models.Relationships;
using C4Sharp.Sample.Structures;

namespace C4Sharp.Sample.Diagrams
{
    using static Systems;    
    using static Components;    
    using static Containers;    
    
    public class ComponentDiagramBuildRunner: DiagramBuildRunner
    {
        protected override string Title => "Internet Banking System API Application";
        protected override DiagramType DiagramType  => DiagramType.Component;
        protected override DiagramLayout FlowVisualization => DiagramLayout.LeftRight;
        protected override bool LayoutAsSketch => true;

        protected override IEnumerable<Structure> Structures() => new Structure[]
        {
            Spa,
            MobileApp,
            SqlDatabase,
            Mainframe,
            Boundary(),
        };

        protected override IEnumerable<Relationship> Relationships() => new Relationship[]
        {
            (Spa > Sign)["Uses", "JSON/HTTPS"],
            (Spa > Accounts)["Uses", "JSON/HTTPS"],
            (MobileApp > Sign)["Uses", "JSON/HTTPS"],
            (MobileApp > Accounts)["Uses", "JSON/HTTPS"],
        };

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