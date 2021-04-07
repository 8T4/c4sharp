using System;
using System.IO;
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
            Save(diagram, "c4");
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
                Directory.CreateDirectory(path);
                var filePath = $"{path}/{diagram.Slug()}.svg";
                File.WriteAllText(filePath, diagram.ToSvg());
            }
            catch (Exception e)
            {
                throw new PlantumlException($"{nameof(PlantumlException)}: Could not save puml file.", e);
            }
        }
    }
}