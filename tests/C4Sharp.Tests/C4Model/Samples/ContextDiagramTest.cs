using System.IO;
using C4Sharp.Models;
using C4Sharp.Models.Diagrams.Core;
using C4Sharp.Models.Plantuml;
using Xunit;
using static C4Sharp.Models.Relationships.Position;
using static C4Sharp.Tests.C4Model.Persons;
using static C4Sharp.Tests.C4Model.Systems;

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