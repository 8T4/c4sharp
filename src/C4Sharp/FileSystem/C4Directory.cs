using System;
using System.IO;
using C4Sharp.Extensions;

namespace C4Sharp.FileSystem
{
    internal static class C4Directory
    {
        public static string DirectoryName => "c4";
        public static string ResourcesFolderName => "resources";
        private static string ResourcesPath => Path.Join(DirectoryName, ResourcesFolderName);      

        public static void LoadResources()
        {
            LoadResource("C4");
            LoadResource("C4_Component");
            LoadResource("C4_Container");
            LoadResource("C4_Context");
            LoadResource("C4_Deployment");
        }
        
        public static void LoadResource(string resourceName)
        {
            try
            {
                var path = Path.Join(ResourcesPath, $"{resourceName}.puml");

                if (File.Exists(path))
                {
                    return;
                }

                var stream = ResourceMethods.GetResource($"{resourceName}.puml");
                Directory.CreateDirectory(ResourcesPath);
                File.WriteAllText(path, stream);
            }
            catch (Exception e)
            {
                throw new C4FileException("An exception occured while the package try loading the resource files", e);
            }
        }
    }
}