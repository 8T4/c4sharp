using System.IO;
using C4Sharp.Models;
using C4Sharp.Models.Diagrams.Core;
using C4Sharp.Models.Plantuml;
using C4Sharp.Models.Relationships;
using Xunit;
using static C4Sharp.Tests.C4Model.Persons;
using static C4Sharp.Tests.C4Model.Systems;
using static C4Sharp.Tests.C4Model.Containers;


namespace C4Sharp.Tests.C4Model.Samples
{
    public class ContainerDiagramTest
    {
        [Fact]
        public void Its_C4_Model_Container_Diagram_Test()
        {
            var diagram = DiagramFixture.BuildContainerDiagram();
            PlantumlFile.Save(diagram);
            
            Assert.True(File.Exists($"c4/{diagram.Slug()}.puml"));
        }
    }
}