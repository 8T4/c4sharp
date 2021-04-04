using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using C4Sharp.Models.Diagrams;

namespace C4Sharp.Models.Plantuml
{
    /// <summary>
    /// PUML File Utils
    /// </summary>
    public static partial class PlantumlFile
    {
        /// <summary>
        /// Save puml file on c4/[file name].puml
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        public static string Save(Diagram diagram)
        {
            Directory.CreateDirectory("c4");
            return Save(diagram, "c4");
        }

        /// <summary>
        /// Save puml file
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        /// <param name="path">Output path</param>
        public static string Save(Diagram diagram, string path)
        {
            var filePath = $"{path}/{diagram.Slug()}.puml";
            File.WriteAllText(filePath, diagram.ToPumlString());
            return filePath;
        }

        /// <summary>
        /// It's creates a PUML file and exports the diagram to PNG File
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        public static PlantumlResult Export(Diagram diagram)
        {
            using (var session = new PlantumlSession())
            {
                Save(diagram);
                var dirPath = Directory.GetCurrentDirectory();
                var umlPath = Path.Join(dirPath, "c4", $"{diagram.Slug()}.puml");
                return Export(umlPath, session);
            }
        }

        /// <summary>
        /// It's creates a PUML file and exports the diagram to PNG File
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        /// <param name="session">Plantuml Session</param>
        public static PlantumlResult Export(Diagram diagram, PlantumlSession session)
        {
            Save(diagram);
            var dirPath = Directory.GetCurrentDirectory();
            var umlPath = Path.Join(dirPath, "c4", $"{diagram.Slug()}.puml");
            return Export(umlPath, session);
        }

        /// <summary>
        /// It's creates a PUML file and exports the diagram to PNG File
        /// </summary>
        /// <param name="diagram"></param>
        /// <param name="path">Output path</param>
        public static PlantumlResult Export(Diagram diagram, string path)
        {
            using (var session = new PlantumlSession())
            {
                Save(diagram, path);
                var umlPath = Path.Join(path, $"{diagram.Slug()}.puml");
                return Export(umlPath, session);
            }
        }

        /// <summary>
        /// It's creates a PUML file and exports the diagram to PNG File
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        /// <param name="path">Output path</param>
        /// <param name="session">Plantuml Session</param>
        public static PlantumlResult Export(Diagram diagram, string path, PlantumlSession session)
        {
            Save(diagram, path);
            var umlPath = Path.Join(path, $"{diagram.Slug()}.puml");
            return Export(umlPath, session);
        }

        /// <summary>
        /// Export Diagram to PNG File
        /// </summary>
        /// <param name="pumlPath">PUML file path</param>
        /// <param name="session">Plantuml Session</param>
        private static PlantumlResult Export(string pumlPath, PlantumlSession session)
        {
            return session.Execute(pumlPath);
        }
    }

    /// <summary>
    /// Async PUML File Utils
    /// </summary>
    public static partial class PlantumlFile
    {
        /// <summary>
        /// Save puml file on c4/[file name].puml 
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<string> SaveAsync(Diagram diagram, CancellationToken cancellationToken)
        {
            return cancellationToken.IsCancellationRequested
                ? Task.FromCanceled<string>(cancellationToken)
                : Task.FromResult(Save(diagram));
        }

        /// <summary>
        /// Save puml file
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        /// <param name="path">Output path</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<string> SaveAsync(Diagram diagram, string path, CancellationToken cancellationToken)
        {
            return cancellationToken.IsCancellationRequested
                ? Task.FromCanceled<string>(cancellationToken)
                : Task.FromResult(Save(diagram, path));
        }
        
        /// <summary>
        /// Export Diagram to PNG File
        /// </summary>
        /// <param name="diagram"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<PlantumlResult> ExportAsync(Diagram diagram, CancellationToken cancellationToken)
        {
            return cancellationToken.IsCancellationRequested 
                ? Task.FromCanceled<PlantumlResult>(cancellationToken) 
                : Task.FromResult(Export(diagram));
        }

        /// <summary>
        /// Export Diagram to PNG File
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        /// <param name="session">Plantuml Session</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<PlantumlResult> ExportAsync(Diagram diagram, PlantumlSession session, CancellationToken cancellationToken)
        {
            return cancellationToken.IsCancellationRequested 
                ? Task.FromCanceled<PlantumlResult>(cancellationToken) 
                : Task.FromResult(Export(diagram, session));            
        }

        /// <summary>
        /// Export Diagram to PNG File
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        /// <param name="path">Output path</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<PlantumlResult> ExportAsync(Diagram diagram, string path, CancellationToken cancellationToken)
        {
            return cancellationToken.IsCancellationRequested 
                ? Task.FromCanceled<PlantumlResult>(cancellationToken) 
                : Task.FromResult(Export(diagram, path));
        }

        /// <summary>
        /// Export Diagram to PNG File
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        /// <param name="path">Output path</param>
        /// <param name="session">Plantuml Session</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<PlantumlResult> ExportAsync(Diagram diagram, string path, PlantumlSession session,
            CancellationToken cancellationToken)
        {
            return cancellationToken.IsCancellationRequested 
                ? Task.FromCanceled<PlantumlResult>(cancellationToken) 
                : Task.FromResult(Export(diagram, path, session));
        }        
    }
}