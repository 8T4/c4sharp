using System.IO;
using C4Sharp.Models;
using C4Sharp.Models.Diagrams.Supplementary;
using C4Sharp.Models.Plantuml;
using C4Sharp.Models.Relationships;
using Xunit;
using static C4Sharp.Tests.C4Model.Nodes;
using static C4Sharp.Tests.C4Model.Containers;

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