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
        /// It creates a Puml file into the default directory "./c4"
        /// If the attribute of Session GenerateDiagramImages is true
        /// It generates png files of the diagram
        /// </summary>
        /// <param name="diagrams">C4 Diagrams</param>
        /// <param name="path">
        /// Full path of the directory
        /// <example>For windows.: C:\users\user\projects\</example>
        /// <example>For Unix.: users/user/projects/</example>
        /// </param>
        /// <param name="session">Plantuml Session</param>
        public static void Export(this PlantumlSession session, string path, IEnumerable<Diagram> diagrams)
        {
            foreach (var diagram in diagrams)
            {
                Save(diagram, path, session);
            }

            session ??= new PlantumlSession();
            
            if (session.GenerateDiagramImages)
            {
                session.Execute(path, true);
            }
        }

        /// <summary>
        /// It creates a Puml file into the default directory "./c4"
        /// If the attribute of Session GenerateDiagramImages is true
        /// It generates png files of the diagram
        /// </summary>
        /// <param name="diagrams">C4 Diagrams</param>
        /// <param name="session">Plantuml Session</param>
        public static void Export(this PlantumlSession session, IEnumerable<Diagram> diagrams)
        {
            foreach (var diagram in diagrams)
            {
                Save(diagram, session);
            }

            var dirPath = Directory.GetCurrentDirectory();
            var path = Path.Join(dirPath, C4Directory.DirectoryName);

            session ??= new PlantumlSession();
            
            if (session.GenerateDiagramImages)
            {
                session.Execute(path, true);
            }
        }

        /// <summary>
        /// Save puml file on c4/[file name].puml
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        /// <param name="session"></param>
        private static void Save(Diagram diagram, PlantumlSession session)
        {
            Save(diagram, C4Directory.DirectoryName, session);
        }

        /// <summary>
        /// Save puml file. It's creates path if non exists.
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        /// <param name="path">Output path</param>
        /// <param name="session"></param>
        private static void Save(Diagram diagram, string path, PlantumlSession session)
        {
            try
            {
                C4Directory.LoadResources(path);
                var filePath = Path.Combine(path, $"{diagram.Slug()}.puml");
                File.WriteAllText(filePath, diagram.ToPumlString(session.StandardLibraryBaseUrl));
            }
            catch (Exception e)
            {
                throw new PlantumlException($"{nameof(PlantumlException)}: Could not save puml file.", e);
            }
        }
    }
}