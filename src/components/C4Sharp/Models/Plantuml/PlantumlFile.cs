using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using C4Sharp.Diagrams;
using C4Sharp.FileSystem;

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
        /// <param name="useStandardLibrary"></param>
        public static void Save(Diagram diagram, bool useStandardLibrary = false)
        {
            Save(diagram, C4Directory.DirectoryName, useStandardLibrary);
        }

        /// <summary>
        /// Save puml file. It's creates path if non exists.
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        /// <param name="path">Output path</param>
        /// <param name="useStandardLibrary"></param>
        public static void Save(Diagram diagram, string path, bool useStandardLibrary = false)
        {
            try
            {
                C4Directory.LoadResources(path);
                var filePath = Path.Combine(path, $"{diagram.Slug()}.puml");
                File.WriteAllText(filePath, diagram.ToPumlString(useStandardLibrary));
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
        /// <param name="session">Plantuml Session</param>
        public static void Export(Diagram diagram, PlantumlSession session)
        {
            Save(diagram, session.StandardLibraryBaseUrl);
            var dirPath = Directory.GetCurrentDirectory();
            var umlPath = Path.Join(dirPath, C4Directory.DirectoryName, $"{diagram.Slug()}.puml");
            Export(umlPath, session);
        }

        /// <summary>
        /// It's creates a PUML file and exports the diagram to PNG File
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        /// <param name="path">Output path</param>
        /// <param name="session">Plantuml Session</param>
        public static void Export(Diagram diagram, string path, PlantumlSession session)
        {
            Save(diagram, path, session.StandardLibraryBaseUrl);
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
}