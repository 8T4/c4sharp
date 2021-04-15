using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using C4Sharp.FileSystem;
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
        public static void Save(Diagram diagram)
        {
            Save(diagram, C4Directory.DirectoryName);
        }

        /// <summary>
        /// Save puml file. It's creates path if non exists.
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        /// <param name="path">Output path</param>
        public static void Save(Diagram diagram, string path)
        {
            try
            {
                C4Directory.LoadResources();
                Directory.CreateDirectory(path);
                var filePath = $"{path}/{diagram.Slug()}.puml";
                File.WriteAllText(filePath, diagram.ToPumlString());
            }
            catch (Exception e)
            {
                throw new PlantumlException($"{nameof(PlantumlException)}: Could not save puml file.", e);
            }
        }

        /// <summary>
        /// It's creates a PUML file and exports the diagram to PNG File
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        public static void Export(Diagram diagram)
        {
            Save(diagram);
            var dirPath = Directory.GetCurrentDirectory();
            var umlPath = Path.Join(dirPath, C4Directory.DirectoryName, $"{diagram.Slug()}.puml");
            Export(umlPath, null);
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
            var umlPath = Path.Join(dirPath, C4Directory.DirectoryName, $"{diagram.Slug()}.puml");
            Export(umlPath, session);
        }

        /// <summary>
        /// It's creates a PUML file and exports the diagram to PNG File
        /// </summary>
        /// <param name="diagram"></param>
        /// <param name="path">Output path</param>
        public static void Export(Diagram diagram, string path)
        {
            Save(diagram, path);
            var umlPath = Path.Join(path, $"{diagram.Slug()}.puml");
            Export(umlPath, null);
        }

        /// <summary>
        /// It's creates a PUML file and exports the diagram to PNG File
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        /// <param name="path">Output path</param>
        /// <param name="session">Plantuml Session</param>
        public static void Export(Diagram diagram, string path, PlantumlSession session)
        {
            Save(diagram, path);
            var umlPath = Path.Join(path, $"{diagram.Slug()}.puml");
            Export(umlPath, session);
        }
        
        /// <summary>
        /// It's creates a PUML file and exports the diagram to PNG File
        /// </summary>
        /// <param name="diagrams">C4 Diagrams</param>
        /// <param name="path">Output path</param>
        /// <param name="session">Plantuml Session</param>
        public static void Export(IEnumerable<Diagram> diagrams, string path, PlantumlSession session)
        {
            foreach (var diagram in diagrams)
            {
                Save(diagram, path);
            }
            
            session ??= new PlantumlSession();
            session.Execute(path, true);            
        }     
        
        /// <summary>
        /// It's creates a PUML file and exports the diagram to PNG File
        /// </summary>
        /// <param name="diagrams">C4 Diagrams</param>
        /// <param name="path">Output path</param>
        /// <param name="session">Plantuml Session</param>
        public static void Export(IEnumerable<Diagram> diagrams, PlantumlSession session)
        {
            foreach (var diagram in diagrams)
            {
                Save(diagram);
            }
            
            var dirPath = Directory.GetCurrentDirectory();            
            var path = Path.Join(dirPath, C4Directory.DirectoryName);    
            
            session ??= new PlantumlSession();
            session.Execute(path, true);            
        }         

        /// <summary>
        /// Export Diagram to PNG File
        /// </summary>
        /// <param name="pumlPath">PUML file path</param>
        /// <param name="session">Plantuml Session</param>
        private static void Export(string pumlPath, PlantumlSession session)
        {
            session ??= new PlantumlSession();
            session.Execute(pumlPath, false);
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
        public static Task SaveAsync(Diagram diagram, CancellationToken cancellationToken)
        {
            return cancellationToken.IsCancellationRequested
                ? Task.FromCanceled<string>(cancellationToken)
                : Task.CompletedTask;
        }

        /// <summary>
        /// Save puml file
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        /// <param name="path">Output path</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task SaveAsync(Diagram diagram, string path, CancellationToken cancellationToken)
        {
            return cancellationToken.IsCancellationRequested
                ? Task.FromCanceled<string>(cancellationToken)
                : Task.CompletedTask;
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
                Task.FromCanceled(cancellationToken);
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
        public static Task ExportAsync(Diagram diagram, PlantumlSession session,
            CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                Task.FromCanceled(cancellationToken);
            }

            Export(diagram, session);
            return Task.CompletedTask;            
        }

        /// <summary>
        /// Export Diagram to PNG File
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        /// <param name="path">Output path</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task ExportAsync(Diagram diagram, string path,
            CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                Task.FromCanceled(cancellationToken);
            }

            Export(diagram, path);
            return Task.CompletedTask;            
        }

        /// <summary>
        /// Export Diagram to PNG File
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        /// <param name="path">Output path</param>
        /// <param name="session">Plantuml Session</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task ExportAsync(Diagram diagram, string path, PlantumlSession session,
            CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                Task.FromCanceled(cancellationToken);
            }

            Export(diagram, path, session);
            return Task.CompletedTask;               
        }
    }
}