using System.IO;
using C4Sharp.Models;
using C4Sharp.Models.Diagrams.Supplementary;
using C4Sharp.Models.Plantuml;
using C4Sharp.Models.Relationships;
using Xunit;
using static C4Sharp.Tests.C4Model.Nodes;
using static C4Sharp.Tests.C4Model.Containers;

namespace C4Sharp.Tests.C4Model.Samples
{
    public class DeploymentDiagramTest
    {
        [Fact]
        public void Its_C4_Model_Deployment_Diagram()
        {
            var db1 = OracleDatabaseInstance("db01");
            var db2 = OracleDatabaseInstance("db02");
            
            var bigBankPlc = new DeploymentNode("plc", "Big Bank plc", "Big Bank plc data center")
            {
                Nodes = new[]
                {
                    Ubuntu("dn", "bigbank-api***\tx8", ApacheTomCat("apache", BackendApi)),
                    Ubuntu("bigbankdb01", "bigbank-db01", OracleNode("oracle", db1)),
                    Ubuntu("bigbankdb02", "bigbank-db02", OracleNode("oracle2", db2)),
                    Ubuntu("bb2", "bigbank-web***\tx4", ApacheTomCat("apache2", WebApp)),
                }
            };

            var diagram = new DeploymentDiagram
            {
                Title = "System Context diagram for Internet Banking System",
                Structures = new Structure[]
                {
                    bigBankPlc,
                    Ios("ios", MobileApp),
                    PersonalComputer("computer", Browser("browser", Spa))
                },
                Relationships = new[]
                {
                    (MobileApp > BackendApi)["Makes API calls to", "json/HTTPS"],
                    (Spa > BackendApi)["Makes API calls to", "json/HTTPS"],
                    (WebApp > Spa)["Delivers to the customer's web browser"][Position.Up],
                    (BackendApi > db1)["Writes to", "JDBC"],
                    (BackendApi < db2)["Reads from", "JDBC"],
                    (db1 > db2)["Replicates data to", "JDBC"][Position.Right],
                }
            };

            PlantumlFile.Save(diagram);
            PlantumlFile.Export(diagram);

            Assert.True(File.Exists($"c4/{diagram.Slug()}.puml"));
        }
    }
}