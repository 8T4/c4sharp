using System.IO;
using C4Sharp.Diagrams;
using C4Sharp.IntegratedTests.Stubs.Diagrams;
using C4Sharp.Models.Plantuml;
using Xunit;

namespace C4Sharp.IntegratedTests
{
    
    public class ExportingDiagramTests: ExportingDiagramFixture
    {
        [Fact]
        public void TestExporteWithoutImages()
        {
            Setup();
            
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
            
            CleanUp();
        }         
        
        [Fact]
        public void TestExportToEspecifiedPath()
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
            VerifyIfPumlFilesExists("diagram",path);
            VerifyIfPngFilesExists("diagram", path);
            
            CleanUp(path);
        }        
        
        [Fact]
        public void TestExportToDefaultPath()
        {
            Setup();
            
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
            
            CleanUp();
        }
    }
}