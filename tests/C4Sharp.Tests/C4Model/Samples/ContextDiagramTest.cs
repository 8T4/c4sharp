using System.IO;
using C4Sharp.Models.Plantuml;
using C4Sharp.Models.SVG;
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
        
        [Fact]
        public void Its_C4_Model_Context_Diagram_SVG()
        {
            var diagram = DiagramFixture.BuildContextDiagram();
            SvgFile.Save(diagram);

            Assert.True(File.Exists($"c4/{diagram.Slug()}.svg"));
        }           
    }
}