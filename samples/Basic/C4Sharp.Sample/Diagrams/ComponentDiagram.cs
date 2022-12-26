using C4Sharp.Diagrams;
using C4Sharp.Elements;
using C4Sharp.Elements.Boundaries;
using C4Sharp.Elements.Relationships;
using C4Sharp.Sample.Structures;

namespace C4Sharp.Sample.Diagrams
{
    using static Systems;    
    using static Components;    
    using static Containers;    
    
    public class ComponentDiagram: DiagramBuildRunner
    {
        protected override string Title => "Internet Banking System API Application";

        protected override string Description =>
            "The word `component` is a hugely overloaded term in the software development industry, but in this context a " +
            "component is a grouping of related functionality encapsulated behind a well-defined interface. If you're using " +
            "a language like Java or C#, the simplest way to think of a component is that it's a collection of implementation " +
            "classes behind an interface. Aspects such as how those components are packaged (e.g. one component vs many " +
            "components per JAR file, DLL, shared library, etc) is a separate and orthogonal concern. " +
            "An important point to note here is that all components inside a container typically execute in the same process " +
            "space. In the C4 model, components are not separately deployable units.";
         
        protected override DiagramType DiagramType  => DiagramType.Component;
        protected override DiagramLayout FlowVisualization => DiagramLayout.LeftRight;
        protected override bool LayoutAsSketch => true;

        protected override IEnumerable<Structure> Structures => new Structure[]
        {
            Spa,
            MobileApp,
            SqlDatabase,
            Mainframe,
            Boundary(),
        };

        protected override IEnumerable<Relationship> Relationships => new Relationship[]
        {
            Spa > Sign | ("Uses", "JSON/HTTPS"),
            Spa > Accounts | ("Uses", "JSON/HTTPS"),
            MobileApp > Sign | ("Uses", "JSON/HTTPS"),
            MobileApp > Accounts | ("Uses", "JSON/HTTPS")
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
                    Security > SqlDatabase | ("Read & write to", "JDBC"),
                    MainframeFacade > Mainframe | ("Uses", "XML/HTTPS")
                }
            };
        }
    }
}