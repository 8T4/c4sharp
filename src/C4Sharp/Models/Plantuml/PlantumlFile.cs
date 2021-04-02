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
        public static void Save(Diagram diagram)
        {
            Directory.CreateDirectory("c4");
            File.WriteAllText($"c4/{diagram.Slug()}.puml", diagram.ToString());
        }

        /// <summary>
        /// Save puml file on c4/[file name].puml 
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task SaveAsync(Diagram diagram, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled(cancellationToken);
            }

            Save(diagram);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Export Diagram to PNG File
        /// </summary>
        /// <param name="diagram"></param>
        public static void Export(Diagram diagram)
        {
            var dirPath = Directory.GetCurrentDirectory();
            var jarPath = Path.Join(dirPath, "bin", "plantuml.jar");
            var umlPath = Path.Join(dirPath, "c4", $"{diagram.Slug()}.puml");

            var jar = $"-jar {jarPath} -charset UTF-8";

            var info = new ProcessStartInfo
            {
                UseShellExecute = false,
                FileName = "java",
                Arguments = $"{jar} {umlPath}"
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
    }
}