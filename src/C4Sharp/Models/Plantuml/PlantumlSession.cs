using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using C4Sharp.Models.Diagrams;

namespace C4Sharp.Models.Plantuml
{
    public class PlantumlSession: IDisposable
    {
        private string FilePath { get; }
        private ProcessStartInfo ProcessInfo { get; }

        public PlantumlSession()
        {
            FilePath = PlantumlStream.LoadPlantUmlEngine();
            ProcessInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                FileName = "java",
            };            
        }

        public PlantumlSessionResult Execute(string puml)
        {
            var directory = new FileInfo(puml);

            if (directory.Directory != null)
            {
                var results = new StringBuilder();
                
                var jar = $"-jar {FilePath} -verbose -o \"{directory.Directory.FullName}\" -charset UTF-8";
                ProcessInfo.Arguments = $"{jar} {puml}";
                ProcessInfo.RedirectStandardOutput = true;
                ProcessInfo.StandardOutputEncoding = Encoding.UTF8;

                var process = new Process {StartInfo = ProcessInfo};

                process.OutputDataReceived += (sender, args) =>
                {
                    results.AppendLine(args.Data);
                };

                process.Start();
                process.WaitForExit();

                return new PlantumlSessionResult(process.ExitCode == 0, results.ToString());
            }

            return new PlantumlSessionResult(false, "Plantuml file not found");
        }

        public void Dispose()
        {
            PlantumlStream.RemovePlantUmlEngine(FilePath);
        }
    }
}