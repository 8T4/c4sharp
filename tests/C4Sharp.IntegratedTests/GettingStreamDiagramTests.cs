using C4Sharp.IntegratedTests.Stubs.Diagrams;
using C4Sharp.Models.Plantuml.IO;
using FluentAssertions;
using Xunit;

namespace C4Sharp.IntegratedTests;

public class GettingStreamDiagramTests
{
    [Fact]
    public void TestGetStream()
    {
        var diagram = ContextDiagramBuilder.Build() with { Title = "Diagram" };

        var session = new PlantumlSession();
        var (_, results) = session.GetStream(diagram);

        results.Should().NotBeNull();
    }
}
