using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace C4Sharp.Models.Plantuml
{
    /// <summary>
    /// Session
    /// </summary>
    public class PlantumlSession: IDisposable
    {
        private string FilePath { get; }
        private ProcessStartInfo ProcessInfo { get; }

        /// <summary>
        /// Constructor
        /// </summary>
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
        
        /// <summary>
        /// Constructor with credentials to impersonate
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
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
        
        /// <summary>
        /// Execute plantuml.jar
        /// </summary>
        /// <param name="path">puml files path</param>
        /// <param name="processWholeDirectory">process all *.puml files</param>
        /// <exception cref="PlantumlException"></exception>
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

        /// <summary>
        /// Clear Plantuml Resource
        /// </summary>
        public void Dispose()
        {
            PlantumlResource.Clear(FilePath);
        }
    }
}