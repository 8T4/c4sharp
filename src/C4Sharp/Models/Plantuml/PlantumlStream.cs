using System.IO;
using System.Reflection;
using C4Sharp.Models.Diagrams;

namespace C4Sharp.Models.Plantuml
{
    public static class PlantumlStream
    {
        public static string LoadPlantUmlEngine()
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

        public static void RemovePlantUmlEngine(string file)
        {
            File.Delete(file);
        }

        private static Stream GetResource()
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream($"C4Sharp.bin.plantuml.jar");
        }

        private static void CopyStream(Stream input, Stream output)
        {
            var buffer = new byte[16 * 1024];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }
    }
}