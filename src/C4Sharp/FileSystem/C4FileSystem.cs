using System;
using System.IO;
using C4Sharp.Models.Diagrams;
using C4Sharp.Models.Plantuml;
using C4Sharp.Models.SVG;

namespace C4Sharp.FileSystem
{
    internal static class C4FileSystem
    {
        public static string DefaultDirectory => "c4";
        
        /// <summary>
        /// Save puml file. It's creates path if non exists.
        /// </summary>
        /// <param name="diagram">C4 Diagram</param>
        /// <param name="path">Output path</param>
        /// <param name="extension">File extension</param>
        public static void Save(Diagram diagram, string path, C4FileType extension)
        {
            try
            {
                Directory.CreateDirectory(path);
                var filePath = $"{path}/{diagram.Slug()}.{extension}";

                switch (extension)
                {
                    case C4FileType.Puml: File.WriteAllText(filePath, diagram.ExportToPuml()); break;
                    case C4FileType.Svg: File.WriteAllText(filePath, diagram.ExportToSvg()); break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(extension), extension, null);
                }
            }
            catch (Exception e)
            {
                throw new C4FileException($"{nameof(PlantumlException)}: Could not save {extension} file.", e);
            }
        }        
    }
}