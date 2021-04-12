using System.IO;
using C4Sharp.Extensions;
using C4Sharp.Models.Diagrams;

namespace C4Sharp.FileSystem
{
    internal static class C4Directory
    {
        public static string DirectoryName => "c4";
        public static string ResourcesFolderName => "resources";
        private static string ResourcesPath => Path.Join(DirectoryName, ResourcesFolderName);      

        public static void LoadResources(Diagram diagram)
        {
            LoadBaseResourceFile();
            var path = Path.Join(ResourcesPath, $"{diagram.Name}.puml");

            if (File.Exists(path))
                return;

            var stream = ResourceMethods.GetResource($"{diagram.Name}.puml");
            Directory.CreateDirectory(ResourcesPath);
            File.WriteAllText(path, stream);
        }

        private static void LoadBaseResourceFile()
        {
            var path = Path.Join(ResourcesPath, $"C4.puml");

            if (File.Exists(path))
                return;

            var stream = ResourceMethods.GetResource($"C4.puml");
            Directory.CreateDirectory(ResourcesPath);
            File.WriteAllText(path, stream);            
        }
    }
}