using C4Sharp.Diagrams;
using C4Sharp.Diagrams.Plantuml;
using Xunit;
using ComponentDiagram = C4Sharp.Sample.Diagrams.ComponentDiagram;
using ContainerDiagram = C4Sharp.Sample.Diagrams.ContainerDiagram;
using ContextDiagram = C4Sharp.Sample.Diagrams.ContextDiagram;
using DeploymentDiagram = C4Sharp.Sample.Diagrams.DeploymentDiagram;

namespace C4Sharp.IntegratedTests;

public class ExportingDiagramTests : ExportingDiagramFixture, IAsyncLifetime
{
    [Fact]
    public void TestExportWithoutImages()
    {
        var diagrams = new Diagram[]
        {
            new ContextDiagram().Build() with { Title = "Diagram" },
            new ContainerDiagram().Build() with { Title = "Diagram" },
            new ComponentDiagram().Build() with { Title = "Diagram" },
            new DeploymentDiagram().Build() with { Title = "Diagram" }
        };

        new PlantumlContext().Export(diagrams);

        VerifyIfResourceFilesExists();
        VerifyIfPumlFilesExists("diagram");
        VerifyIfPngFilesNonExists("diagram");
    }

    [Fact]
    public void TestExportToSpecifiedPath()
    {
        const string path = "c4temp";
        Setup(path);

        var diagrams = new Diagram[]
        {
            new ContextDiagram().Build() with { Title = "Diagram" },
            new ContainerDiagram().Build() with { Title = "Diagram" },
            new ComponentDiagram().Build() with { Title = "Diagram" },
            new DeploymentDiagram().Build() with { Title = "Diagram" }
        };

        var pathSave = new DirectoryInfo(path).FullName;
        new PlantumlContext()
            .UseDiagramImageBuilder()
            .Export(pathSave, diagrams);

        VerifyIfResourceFilesExists(path);
        VerifyIfPumlFilesExists("diagram", path);
        VerifyIfPngFilesExists("diagram", path);

        CleanUp(path);
    }

    [Fact]
    public void TestExportOnlyPngToDefaultPath()
    {
        var diagrams = new Diagram[]
        {
            new ContextDiagram().Build() with { Title = "Diagram" },
            new ContainerDiagram().Build() with { Title = "Diagram" },
            new ComponentDiagram().Build() with { Title = "Diagram" },
            new DeploymentDiagram().Build() with { Title = "Diagram" }
        };

        new PlantumlContext()
            .UseDiagramImageBuilder()
            .Export(diagrams);

        VerifyIfResourceFilesExists();
        VerifyIfPumlFilesExists("diagram");
        VerifyIfPngFilesExists("diagram");
        VerifyIfSvgFilesNonExists("diagram");
    }

    [Fact]
    public void TestExportOnlySvgToDefaultPath()
    {
        var diagrams = new Diagram[]
        {
            new ContextDiagram().Build() with { Title = "Diagram" },
            new ContainerDiagram().Build() with { Title = "Diagram" },
            new ComponentDiagram().Build() with { Title = "Diagram" },
            new DeploymentDiagram().Build() with { Title = "Diagram" }
        };

        new PlantumlContext()
            .UseDiagramSvgImageBuilder()
            .Export(diagrams);

        VerifyIfResourceFilesExists();
        VerifyIfPumlFilesExists("diagram");
        VerifyIfPngFilesNonExists("diagram");
        VerifyIfSvgFilesExists("diagram");
    }

    [Fact]
    public void TestExportPngAndSvgToDefaultPath()
    {
        var diagrams = new Diagram[]
        {
            new ContextDiagram().Build() with { Title = "Diagram" },
            new ContainerDiagram().Build() with { Title = "Diagram" },
            new ComponentDiagram().Build() with { Title = "Diagram" },
            new DeploymentDiagram().Build() with { Title = "Diagram" }
        };

        new PlantumlContext()
            .UseDiagramImageBuilder()
            .UseDiagramSvgImageBuilder()
            .Export(diagrams);

        VerifyIfResourceFilesExists();
        VerifyIfPumlFilesExists("diagram");
        VerifyIfPngFilesExists("diagram");
        VerifyIfSvgFilesExists("diagram");
    }

    public Task InitializeAsync()
    {
        Setup();
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        CleanUp();
        return Task.CompletedTask;
    }
}
