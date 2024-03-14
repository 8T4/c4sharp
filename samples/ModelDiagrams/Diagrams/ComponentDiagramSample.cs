using C4Sharp.Diagrams;
using C4Sharp.Diagrams.Builders;
using C4Sharp.Elements;
using C4Sharp.Elements.Relationships;
using static ModelDiagrams.Structures.Systems;
using static ModelDiagrams.Structures.Containers;
using static ModelDiagrams.Structures.Components;

namespace ModelDiagrams.Diagrams;

public class ComponentDiagramSample : ComponentDiagram
{
    protected override string Title => "Internet Banking System API Application";
    protected override DiagramLayout FlowVisualization => DiagramLayout.LeftRight;

    protected override IEnumerable<Structure> Structures => new Structure[]
    {
        Spa,
        MobileApp,
        SqlDatabase,
        Mainframe,
        Boundary("c1", "API Application",
            Sign,
            Accounts,
            Security,
            MainframeFacade
        ),
    };

    protected override IEnumerable<Relationship> Relationships => new Relationship[]
    {
        Sign > Security,
        Accounts > MainframeFacade,
        Security > SqlDatabase | ("Read & write to", "JDBC"),
        MainframeFacade > Mainframe | ("Uses", "XML/HTTPS"),

        Spa > Sign | ("Uses", "JSON/HTTPS"),
        Spa > Accounts | ("Uses", "JSON/HTTPS"),
        MobileApp > Sign | ("Uses", "JSON/HTTPS"),
        MobileApp > Accounts | ("Uses", "JSON/HTTPS")
    };
}