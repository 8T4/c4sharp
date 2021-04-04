using System;
using System.Diagnostics;
using System.IO;
using System.Security;
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
                FileName = "java",
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden,
            };            
        }
        
        public PlantumlSession(string username, string password)
        {
            FilePath = PlantumlResource.Load();
            ProcessInfo = new ProcessStartInfo
            {
                FileName = "java",
                UserName = username,
                UseShellExecute = false,
                PasswordInClearText = password,
                WindowStyle = ProcessWindowStyle.Hidden,
            };            
        }        
        
        internal void Execute(string path, bool processWholeDirectory)
        {
            var directory = processWholeDirectory 
                ? path
                : new FileInfo(path)?.Directory?.FullName;

            try
            {
                if (string.IsNullOrEmpty(directory))
                {
                    throw new PlantumlException($"{nameof(PlantumlException)}: puml file not found.");
                }

                var results = new StringBuilder();

                var jar = $"-jar {FilePath} -verbose -o \"{directory}\" -charset UTF-8";
                ProcessInfo.Arguments = $"{jar} {path}";
                ProcessInfo.RedirectStandardOutput = true;
                ProcessInfo.StandardOutputEncoding = Encoding.UTF8;

                var process = new Process {StartInfo = ProcessInfo};

                process.OutputDataReceived += (sender, args) => { results.AppendLine(args.Data); };

                process.Start();
                process.WaitForExit();
            }
            catch (Exception e)
            {
                throw new PlantumlException($"{nameof(PlantumlException)}: puml file not found.", e);
            }
        }

        public void Dispose()
        {
            PlantumlResource.Clear(FilePath);
        }
    }
}