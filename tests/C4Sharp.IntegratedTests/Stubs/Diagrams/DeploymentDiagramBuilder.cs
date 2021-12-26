using C4Sharp.Diagrams.Supplementary;
using C4Sharp.IntegratedTests.Stubs.Models;
using C4Sharp.Models;
using C4Sharp.Models.Relationships;

namespace C4Sharp.IntegratedTests.Stubs.Diagrams;

public static class DeploymentDiagramBuilder
{
    public static DeploymentDiagram Build() => new()
    {
        Title = "System Context diagram for Internet Banking System",
        Structures = new Structure[]
        {
                BigBankNode(),
                Nodes.Ios("ios", Containers.MobileApp),
                Nodes.PersonalComputer("computer", Nodes.Browser("browser", Containers.Spa))
        },
        Relationships = new[]
        {
            (Containers.MobileApp > Containers.BackendApi)["Makes API calls to", "json/HTTPS"],
            (Containers.Spa > Containers.BackendApi)["Makes API calls to", "json/HTTPS"],
            (Containers.WebApp > Containers.Spa)["Delivers to the customer's web browser"][Position.Up],
            (Containers.BackendApi > Containers.OracleDatabase[1])["Writes to", "JDBC"],
            (Containers.BackendApi < Containers.OracleDatabase[2])["Reads from", "JDBC"],
            (Containers.OracleDatabase[1] > Containers.OracleDatabase[2])["Replicates data to", "JDBC"][Position.Right],
        }
    };

    private static DeploymentNode BigBankNode() => new("plc", "Big Bank plc")
    {
        Description = "Big Bank plc data center",
        Nodes = new[]
        {
            Nodes.Ubuntu("dn", "bigbank-api***\tx8", Nodes.ApacheTomCat("apache", Containers.BackendApi)),
            Nodes.Ubuntu("bigbankdb01", "bigbank-db01", Nodes.OracleNode("oracle", Containers.OracleDatabase[1])),
            Nodes.Ubuntu("bigbankdb02", "bigbank-db02", Nodes.OracleNode("oracle2", Containers.OracleDatabase[2])),
            Nodes.Ubuntu("bb2", "bigbank-web***\tx4", Nodes.ApacheTomCat("apache2", Containers.WebApp)),
        }
    };
}
