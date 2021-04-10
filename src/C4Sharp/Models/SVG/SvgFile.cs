using System;
using System.IO;
using C4Sharp.FileSystem;
using C4Sharp.Models.Diagrams;
using C4Sharp.Models.Plantuml;

namespace C4Sharp.Models.SVG
{
    public static partial class SvgFile
    {
        /// <summary>
        /// Save puml file on c4/[file name].puml
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        public static void Save(Diagram diagram)
        {
            Save(diagram, C4FileSystem.DefaultDirectory);
        }

        /// <summary>
        /// Save puml file. It's creates path if non exists.
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        /// <param name="path">Output path</param>
        public static void Save(Diagram diagram, string path)
        {
            C4FileSystem.Save(diagram, path, C4FileType.Svg);
        }
    }
}