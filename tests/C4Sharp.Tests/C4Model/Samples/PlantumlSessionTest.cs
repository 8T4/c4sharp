using C4Sharp.Models.Diagrams;
using C4Sharp.Models.Plantuml;
using C4Sharp.Tests.C4Model.Fixtures;
using Xunit;

namespace C4Sharp.Tests.C4Model.Samples
{
    public class PlantumlSessionTest
    {
        [Fact]
        public void It_Exports_Core_Diagram()
        {
            var diagrams = new Diagram[]
            {
                DiagramFixture.BuildContextDiagram(),
                DiagramFixture.BuildContainerDiagram(),
                DiagramFixture.BuildComponentDiagram(),
                DiagramFixture.BuildDeploymentDiagram()
            };

            using (var session = new PlantumlSession())
            {
                PlantumlFile.Export(diagrams, session);
            }

            Assert.True(true);
        }         
    }
}