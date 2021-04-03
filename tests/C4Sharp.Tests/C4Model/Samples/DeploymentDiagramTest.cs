using System.IO;
using C4Sharp.Models.Plantuml;
using Xunit;

namespace C4Sharp.Tests.C4Model.Samples
{
    public class DeploymentDiagramTest
    {
        [Fact]
        public void Its_C4_Model_Deployment_Diagram()
        {
            var diagram = DiagramFixture.BuildDeploymentDiagram();
            PlantumlFile.Save(diagram);

            Assert.True(File.Exists($"c4/{diagram.Slug()}.puml"));
        }
    }
}