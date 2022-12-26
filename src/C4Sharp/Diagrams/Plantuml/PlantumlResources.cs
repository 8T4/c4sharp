using C4Sharp.Commons;
using C4Sharp.FileSystem;

namespace C4Sharp.Elements.Plantuml.IO;

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

        LoadResource(local, "C4.puml");
        LoadResource(local, "C4_Component.puml");
        LoadResource(local, "C4_Container.puml");
        LoadResource(local, "C4_Context.puml");
        LoadResource(local, "C4_Deployment.puml");
    }
    
    /// <summary>
    /// Load all C4_Plantuml files
    /// </summary>
    public static void LoadHtmlResources(string path)
    {
        LoadResource(path, "ds.js");
        LoadIconPngResource(path);
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
            var path = Path.Join(resourcesPath, resourceName);

            if (File.Exists(path))
            {
                return;
            }

            var stream = ResourceFile.ReadString(resourceName);
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
            
            LoadStream(fileName, resourceName);

            return fileName;
        }
        catch (Exception e)
        {
            throw new PlantumlException($"{nameof(PlantumlException)}: Could not load plantuml engine.", e);
        }
    }
    
    /// <summary>
    /// Load Icon PNG file
    /// </summary>
    /// <param name="resourcesPath"></param>
    /// <exception cref="C4FileException"></exception>
    private static void LoadIconPngResource(string resourcesPath)
    {
        try
        {
            const string resourceName = "icon.png";
            var path = Path.Join(resourcesPath, resourceName);

            if (File.Exists(path))
            {
                return;
            }
            
            LoadStream(path, resourceName);
        }
        catch (Exception e)
        {
            throw new PlantumlException($"{nameof(PlantumlException)}: Could not load plantuml engine.", e);
        }
    }

    private static void LoadStream(string path, string resourceName)
    {
        var stream = ResourceFile.ReadStream(resourceName) ?? throw new InvalidOperationException();
        using var file = new FileStream(path, FileMode.Create, FileAccess.Write);
        stream.CopyTo(file);
        stream.Flush();        
    }
}
