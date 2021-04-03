using System.IO;
using C4Sharp.Models.Plantuml;
using Xunit;

namespace C4Sharp.Tests.C4Model.Samples
{
    public class ContextDiagramTest
    {
        [Fact]
        public void Its_C4_Model_Context_Diagram()
        {
            var diagram = DiagramFixture.BuildContextDiagram();
            PlantumlFile.Save(diagram);

            Assert.True(File.Exists($"c4/{diagram.Slug()}.puml"));
        }        
    }
}