using System.IO;
using C4Sharp.Models.Plantuml;
using Xunit;


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