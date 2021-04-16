using System;
using System.IO;
using C4Sharp.Extensions;

namespace C4Sharp.FileSystem
{
    /// <summary>
    /// Manipulate the C4 folder and their resoucers
    /// </summary>
    internal static class C4Directory
    {
        /// <summary>
        /// Default Directory Name
        /// </summary>
        public static string DirectoryName => "c4";
        /// <summary>
        /// Default Resource Folder Name
        /// </summary>
        public static string ResourcesFolderName => "resources";
        /// <summary>
        /// Default Resource path
        /// </summary>
        private static string ResourcesPath => Path.Join(DirectoryName, ResourcesFolderName);      

        /// <summary>
        /// Load all C4_Plantuml files
        /// </summary>
        public static void LoadResources()
        {
            LoadResource("C4");
            LoadResource("C4_Component");
            LoadResource("C4_Container");
            LoadResource("C4_Context");
            LoadResource("C4_Deployment");
        }
        
        /// <summary>
        /// Load C4_Plantuml file
        /// </summary>
        /// <param name="resourceName"></param>
        /// <exception cref="C4FileException"></exception>
        private static void LoadResource(string resourceName)
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