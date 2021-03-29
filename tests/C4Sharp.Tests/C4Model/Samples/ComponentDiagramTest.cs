using System.IO;
using C4Sharp.Models;
using C4Sharp.Models.Diagrams;
using C4Sharp.Models.Diagrams.Core;
using C4Sharp.Models.Plantuml;
using C4Sharp.Models.Relationships;
using Xunit;
using static C4Sharp.Tests.C4Model.Components;
using static C4Sharp.Tests.C4Model.Containers;
using static C4Sharp.Tests.C4Model.Systems;


namespace C4Sharp.Tests.C4Model.Samples
{
    public class ComponentDiagramTest
    {
        [Fact]
        public void Its_C4_Model_Component_Diagram_Test()
        {
            var boundary = new ContainerBoundary("c1", "API Application")
            { 
                Components = new[]
                {
                    Sign,
                    Accounts,
                    Security,
                    MainframeFacade
                },
                Relationships = new []
                {
                    Sign > Security,
                    Accounts > MainframeFacade,
                    (Security > SqlDatabase) ["Read & write to", "JDBC"],
                    (MainframeFacade > Mainframe)["Uses", "XML/HTTPS"]
                }
            };


            var diagram = new ComponentDiagram()
            {
                Title = "Component diagram for Internet Banking System - API Application",
                Structures = new Structure[]
                {
                    Spa,
                    MobileApp,
                    SqlDatabase,
                    Mainframe,
                    boundary,
                },
                Relationships = new[]
                {
                    (Spa > Sign)["Uses", "JSON/HTTPS"],
                    (Spa > Accounts)["Uses", "JSON/HTTPS"],
                    (MobileApp > Sign)["Uses", "JSON/HTTPS"],
                    (MobileApp > Accounts)["Uses", "JSON/HTTPS"],
                }
            };

            PlantumlFile.Save(diagram);
            PlantumlFile.Export(diagram);

            Assert.True(File.Exists($"c4/{diagram.Slug()}.puml"));
        }
    }
}