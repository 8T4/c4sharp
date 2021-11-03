using C4Sharp.Diagrams;
using C4Sharp.Diagrams.Core;
using FluentAssertions;
using Xunit;

namespace C4Sharp.Tests.Diagrams
{
    public class DiagramTests
    {
        [Theory]
        [InlineData("Test A")]
        [InlineData("test A")]
        [InlineData("TEST A")]
        public void TestWhenSlugComponentDiagram(string title)
        {
            var diagram = Stubs.Diagrams.ComponentDiagramBuilder.Build() with { Title = title};
            diagram.Slug().Should().Be("test-a-c4component");
        }
        
        [Theory]
        [InlineData("Test A")]
        [InlineData("test A")]
        [InlineData("TEST A")]
        public void TestWhenSlugContextDiagram(string title)
        {
            var diagram = Stubs.Diagrams.ContextDiagramBuilder.Build() with { Title = title};
            diagram.Slug().Should().Be("test-a-c4context");
        }   
        
        [Theory]
        [InlineData("Test A")]
        [InlineData("test A")]
        [InlineData("TEST A")]
        public void TestWhenSlugContainerDiagram(string title)
        {
            var diagram = Stubs.Diagrams.ContainerDiagramBuilder.Build() with { Title = title};
            diagram.Slug().Should().Be("test-a-c4container");
        }  
        
        [Theory]
        [InlineData("Test A")]
        [InlineData("test A")]
        [InlineData("TEST A")]
        public void TestWhenSlugDeploymentDiagram(string title)
        {
            var diagram = Stubs.Diagrams.DeploymentDiagramBuilder.Build() with { Title = title};
            diagram.Slug().Should().Be("test-a-c4deployment");
        }         

        [Fact]
        public void TestWhenCreateWithDefaultAttributes()
        {
            var diagram = new ComponentDiagram();

            diagram.LayoutWithLegend.Should().BeTrue();
            diagram.LayoutAsSketch.Should().BeFalse();
            diagram.ShowLegend.Should().BeFalse();
            diagram.FlowVisualization.Should().Be(DiagramLayout.TopDown);
            diagram.Structures.Should().BeNullOrEmpty();
            diagram.Relationships.Should().BeNullOrEmpty();
        }
    }
}