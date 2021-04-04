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
            FilePath = PlantumlResource.Load();
            ProcessInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                FileName = "java",
            };            
        }

        internal PlantumlResult Execute(string puml)
        {
            var directory = new FileInfo(puml);

            try
            {
                if (directory.Directory == null)
                {
                    return new PlantumlResult(false, "Plantuml file not found");
                }

                var results = new StringBuilder();

                var jar = $"-jar {FilePath} -verbose -o \"{directory.Directory.FullName}\" -charset UTF-8";
                ProcessInfo.Arguments = $"{jar} {puml}";
                ProcessInfo.RedirectStandardOutput = true;
                ProcessInfo.StandardOutputEncoding = Encoding.UTF8;

                var process = new Process {StartInfo = ProcessInfo};

                process.OutputDataReceived += (sender, args) => { results.AppendLine(args.Data); };

                process.Start();
                process.WaitForExit();

                return new PlantumlResult(process.ExitCode == 0, results.ToString());
            }
            catch (Exception e)
            {
                return new PlantumlResult(false, $"{e.Message}\r\n{ e.StackTrace}");
            }
        }

        public void Dispose()
        {
            PlantumlResource.Clear(FilePath);
        }
    }
}