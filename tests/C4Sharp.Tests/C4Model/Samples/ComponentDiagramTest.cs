using System.IO;
using C4Sharp.Models;
using C4Sharp.Models.Diagrams.Core;
using C4Sharp.Models.Plantuml;
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
            var diagram = DiagramFixture.BuildComponentDiagram();
            PlantumlFile.Save(diagram);

            Assert.True(File.Exists($"c4/{diagram.Slug()}.puml"));
        }
    }
}