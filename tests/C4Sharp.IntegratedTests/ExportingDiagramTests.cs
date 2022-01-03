using C4Sharp.Diagrams;
using C4Sharp.IntegratedTests.Stubs.Diagrams;
using C4Sharp.Models.Plantuml.IO;
using Xunit;

namespace C4Sharp.IntegratedTests;

public class ExportingDiagramTests : ExportingDiagramFixture, IDisposable
{
    public ExportingDiagramTests() => Setup();

    [Fact]
    public void TestExportWithoutImages()
    {
        var diagrams = new Diagram[]
        {
            ContextDiagramBuilder.Build() with { Title = "Diagram" },
            ContainerDiagramBuilder.Build() with { Title = "Diagram" },
            ComponentDiagramBuilder.Build() with { Title = "Diagram" },
            DeploymentDiagramBuilder.Build() with { Title = "Diagram" }
        };

        using var session = new PlantumlSession();
        session.Export(diagrams);

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
            ContextDiagramBuilder.Build() with { Title = "Diagram" },
            ContainerDiagramBuilder.Build() with { Title = "Diagram" },
            ComponentDiagramBuilder.Build() with { Title = "Diagram" },
            DeploymentDiagramBuilder.Build() with { Title = "Diagram" }
        };

        var pathSave = new DirectoryInfo(path).FullName;

        new PlantumlSession()
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
            ContextDiagramBuilder.Build() with { Title = "Diagram" },
            ContainerDiagramBuilder.Build() with { Title = "Diagram" },
            ComponentDiagramBuilder.Build() with { Title = "Diagram" },
            DeploymentDiagramBuilder.Build() with { Title = "Diagram" }
        };

        new PlantumlSession()
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
            ContextDiagramBuilder.Build() with { Title = "Diagram" },
            ContainerDiagramBuilder.Build() with { Title = "Diagram" },
            ComponentDiagramBuilder.Build() with { Title = "Diagram" },
            DeploymentDiagramBuilder.Build() with { Title = "Diagram" }
        };

        new PlantumlSession()
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
            ContextDiagramBuilder.Build() with { Title = "Diagram" },
            ContainerDiagramBuilder.Build() with { Title = "Diagram" },
            ComponentDiagramBuilder.Build() with { Title = "Diagram" },
            DeploymentDiagramBuilder.Build() with { Title = "Diagram" }
        };

        new PlantumlSession()
            .UseDiagramImageBuilder()
            .UseDiagramSvgImageBuilder()
            .Export(diagrams);

        VerifyIfResourceFilesExists();
        VerifyIfPumlFilesExists("diagram");
        VerifyIfPngFilesExists("diagram");
        VerifyIfSvgFilesExists("diagram");
    }

    public void Dispose() => CleanUp();
}
