using C4Sharp.Extensions;
using C4Sharp.FileSystem;

namespace C4Sharp.Models.Plantuml.IO;

internal static class PlantumlResources
{
    /// <summary>
    /// Load all C4_Plantuml files
    /// </summary>
    public static void LoadResources(string path)
    {
        var local = string.IsNullOrEmpty(path)
            ? Path.Join(C4SharpDirectory.DirectoryName, C4SharpDirectory.ResourcesFolderName)
            : Path.Join(path, C4SharpDirectory.ResourcesFolderName);

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

            var stream = ResourceMethods.GetResourceContent($"{resourceName}.puml");
            Directory.CreateDirectory(resourcesPath);
            File.WriteAllText(path, stream);
        }
        catch (Exception e)
        {
            throw new C4FileException("An exception occured while the package try loading the resource files", e);
        }
    }

    /// <summary>
    /// Get Stream from plantuml.jar file
    /// </summary>
    /// <returns>Stream</returns>
    /// <exception cref="PlantumlException"></exception>
    public static string LoadPlantumlJar()
    {
        try
        {
            const string resourceName = "plantuml.jar";
            var fileName = Path.GetTempFileName();

            var stream = ResourceMethods.GetResourceStream(resourceName) ?? throw new InvalidOperationException();
            using var file = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            stream.CopyTo(file);

            return fileName;
        }
        catch (Exception e)
        {
            throw new PlantumlException($"{nameof(PlantumlException)}: Could not load plantuml engine.", e);
        }
    }
}
