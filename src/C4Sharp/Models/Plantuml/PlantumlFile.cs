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
    public static class PlantumlFile
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
        /// <param name="path">Path to save</param>
        public static string Save(Diagram diagram, string path)
        {
            var filePath = $"{path}/{diagram.Slug()}.puml";
            File.WriteAllText(filePath, diagram.ToString());

            return filePath;
        }

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
        /// <param name="path">file path</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task SaveAsync(Diagram diagram, string path, CancellationToken cancellationToken)
        {
            return cancellationToken.IsCancellationRequested
                ? Task.FromCanceled<string>(cancellationToken)
                : Task.FromResult(Save(diagram, path));
        }

        /// <summary>
        /// It's creates a PUML file and exports the diagram to PNG File
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        public static void Export(Diagram diagram)
        {
            using (var session = new PlantumlSession())
            {
                Save(diagram);

                var dirPath = Directory.GetCurrentDirectory();
                var umlPath = Path.Join(dirPath, "c4", $"{diagram.Slug()}.puml");
                Export(umlPath, session);
            }
        }

        /// <summary>
        /// It's creates a PUML file and exports the diagram to PNG File
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        /// <param name="session">Plantuml Session</param>
        public static void Export(Diagram diagram, PlantumlSession session)
        {
            Save(diagram);
            var dirPath = Directory.GetCurrentDirectory();
            var umlPath = Path.Join(dirPath, "c4", $"{diagram.Slug()}.puml");
            Export(umlPath, session);
        }

        /// <summary>
        /// It's creates a PUML file and exports the diagram to PNG File
        /// </summary>
        /// <param name="diagram"></param>
        /// <param name="path">file path</param>
        public static void Export(Diagram diagram, string path)
        {
            using (var session = new PlantumlSession())
            {
                Save(diagram, path);

                var umlPath = Path.Join(path, $"{diagram.Slug()}.puml");
                Export(umlPath, session);
            }
        }

        /// <summary>
        /// It's creates a PUML file and exports the diagram to PNG File
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        /// <param name="path">File path</param>
        /// <param name="session">Plantuml Session</param>
        public static void Export(Diagram diagram, string path, PlantumlSession session)
        {
            Save(diagram, path);
            var umlPath = Path.Join(path, $"{diagram.Slug()}.puml");
            Export(umlPath, session);
        }

        /// <summary>
        /// Export Diagram to PNG File
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="session">Plantuml Session</param>
        private static void Export(string path, PlantumlSession session)
        {
            var jarPath = session.FilePath;
            var directory = new FileInfo(path);

            var jar = $"-jar {jarPath} -verbose -o \"{directory.Directory.FullName}\" -charset UTF-8";

            var info = new ProcessStartInfo
            {
                UseShellExecute = false,
                FileName = "java",
                Arguments = $"{jar} {path}"
            };

            Process.Start(info)?.WaitForExit();
        }

        /// <summary>
        /// Export Diagram to PNG File
        /// </summary>
        /// <param name="diagram"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task ExportAsync(Diagram diagram, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled(cancellationToken);
            }

            Export(diagram);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Export Diagram to PNG File
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        /// <param name="session">Plantuml Session</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task ExportAsync(Diagram diagram, PlantumlSession session, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled(cancellationToken);
            }

            Export(diagram, session);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Export Diagram to PNG File
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        /// <param name="path">file path</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task ExportAsync(Diagram diagram, string path, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled(cancellationToken);
            }

            Export(diagram, path);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Export Diagram to PNG File
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        /// <param name="path">File path</param>
        /// <param name="session">Plantuml Session</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task ExportAsync(Diagram diagram, string path, PlantumlSession session,
            CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled(cancellationToken);
            }

            Export(diagram, path, session);

            return Task.CompletedTask;
        }
    }
}