using C4Sharp.Models.Plantuml;
using Xunit;

namespace C4Sharp.Tests.C4Model.Samples
{
    public class PlantumlSessionTest
    {
        [Fact]
        public void It_Exports_Core_Diagram()
        {
            var context = DiagramFixture.BuildContextDiagram();
            var container = DiagramFixture.BuildContainerDiagram();
            var component = DiagramFixture.BuildComponentDiagram();

            using (var session = new PlantumlSession())
            {
                PlantumlFile.Export(context, session);
                PlantumlFile.Export(container, session);
                PlantumlFile.Export(component, session);
            }

            Assert.True(true);
        }         
    }
}