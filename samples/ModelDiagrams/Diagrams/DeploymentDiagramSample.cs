using C4Sharp.Diagrams;
using C4Sharp.Diagrams.Builders;
using C4Sharp.Elements;
using C4Sharp.Elements.Relationships;
using ModelDiagrams.Structures;

namespace ModelDiagrams.Diagrams;

public class DeploymentDiagramSample: DeploymentDiagram
{
    protected override string Title => "System Context diagram for Internet Banking System";

    protected override IEnumerable<Structure> Structures => new Structure[]
    {
        Node("plc", "Big Bank plc", "Big Bank plc data center",
            Nodes.Ubuntu("dn", "bigbank-api***\tx8", Nodes.ApacheTomCat("apache", Containers.BackendApi)),
            Nodes.Ubuntu("bigbankdb01", "bigbank-db01", Nodes.OracleNode("oracle", Containers.OracleDatabase[1])),
            Nodes.Ubuntu("bigbankdb02", "bigbank-db02", Nodes.OracleNode("oracle2", Containers.OracleDatabase["Data Reader"])),
            Nodes.Ubuntu("bb2", "bigbank-web***\tx4", Nodes.ApacheTomCat("apache2", Containers.WebApp))),
        Nodes.Ios("ios", Containers.MobileApp),
        Nodes.PersonalComputer("computer", Nodes.Browser("browser", Containers.SpaApp))
    };

    protected override IEnumerable<Relationship> Relationships => new[]
    {
        Containers.MobileApp > Containers.BackendApi | ("Makes API calls to", "json/HTTPS"),
        Containers.SpaApp > Containers.BackendApi | ("Makes API calls to", "json/HTTPS"),
        Containers.WebApp > Containers.SpaApp | "Delivers to the customer's web browser"| Position.Up,
        Containers.BackendApi > Containers.OracleDatabase[1] | ("Writes to", "JDBC"),
        Containers.BackendApi < Containers.OracleDatabase["Data Reader"] | ("Reads from", "JDBC"),
        Containers.OracleDatabase[1] > Containers.OracleDatabase["Data Reader"] | ("Replicates data to", "JDBC") | Position.Right,
    };
}