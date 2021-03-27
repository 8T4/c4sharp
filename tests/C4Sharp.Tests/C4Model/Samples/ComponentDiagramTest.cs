using System.IO;
using C4Sharp.Models;
using C4Sharp.Models.Diagrams;
using C4Sharp.Models.Puml;
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
            var boundary = new ContainerBoundary("c1", "API Application", new[]
                {
                    Sign,
                    Accounts,
                    Security,
                    MainframeFacade
                },
                new Relationship[]
                {
                    new Relationship(Sign, Security, "Uses"),
                    new Relationship(Accounts, MainframeFacade, "Uses"),
                    new Relationship(Security, Database, "Read & write to", "JDBC"),
                    new Relationship(MainframeFacade, Mainframe, "Uses", "XML/HTTPS")                    
                }
            );
            
            
            var diagram = new ComponentDiagram()
            {
                Title = "Component diagram for Internet Banking System - API Application",
                Structures = new Structure[]
                {
                    Spa,
                    MobileApp,
                    Database,
                    Mainframe,
                    boundary,
                },
                Relationships = new Relationship[]
                {
                    new Relationship(Spa, Sign, "Uses", "JSON/HTTPS"),
                    new Relationship(Spa, Accounts, "Uses", "JSON/HTTPS"),

                    new Relationship(MobileApp, Sign, "Uses", "JSON/HTTPS"),
                    new Relationship(MobileApp, Accounts, "Uses", "JSON/HTTPS")
                }
            };
            
            PumlFile.Save(diagram);
            PumlFile.ExportToPng(diagram);
            
            Assert.True(File.Exists($"c4/{diagram.Slug()}.puml"));
        }
    }
}