using System.IO;
using System.Reflection;
using C4Sharp.Models.Diagrams;

namespace C4Sharp.Models.Plantuml
{
    /// <summary>
    /// PlantUML Resources
    /// </summary>    
    internal static class PlantumlResource
    {
        public static string Load()
        {
            var fileName = Path.GetTempFileName();

            using (var resource = GetResource())
            {
                using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    resource.CopyTo(file);
                }
            }

            return fileName;
        }

        public static void Clear(string file)
        {
            File.Delete(file);
        }

        private static Stream GetResource()
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream($"C4Sharp.bin.plantuml.jar");
        }
    }
}