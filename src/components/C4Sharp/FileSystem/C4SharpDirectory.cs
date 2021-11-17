using System;
using System.IO;
using C4Sharp.Extensions;

namespace C4Sharp.FileSystem
{
    /// <summary>
    /// Manipulate the C4 folder and their resoucers
    /// </summary>
    internal static class C4SharpDirectory
    {
        /// <summary>
        /// Default Directory Name
        /// </summary>
        public static string DirectoryName => "c4";
        /// <summary>
        /// Default Resource Folder Name
        /// </summary>
        public static string ResourcesFolderName => Path.Join("..", ".c4s");

        /// <summary>
        /// Load all C4_Plantuml files
        /// </summary>
        public static void LoadResources(string path = null)
        {
            var local = path is null
                ? Path.Join(DirectoryName, ResourcesFolderName)
                : Path.Join(path, ResourcesFolderName);
            
            LoadResource(local, "C4");
            LoadResource(local, "C4_Component");
            LoadResource(local, "C4_Container");
            LoadResource(local, "C4_Context");
            LoadResource(local, "C4_Deployment");
        }

        /// <summary>
        /// Load C4_Plantuml file
        /// </summary>
        /// <param name="resourcesPath"></param>
        /// <param name="resourceName"></param>
        /// <exception cref="C4FileException"></exception>
        private static void LoadResource(string resourcesPath, string resourceName)
        {
            try
            {
                var path = Path.Join(resourcesPath, $"{resourceName}.puml");

                if (File.Exists(path))
                {
                    return;
                }

                var stream = ResourceMethods.GetResource($"{resourceName}.puml");
                Directory.CreateDirectory(resourcesPath);
                File.WriteAllText(path, stream);
            }
            catch (Exception e)
            {
                throw new C4FileException("An exception occured while the package try loading the resource files", e);
            }
        }        
    }
}