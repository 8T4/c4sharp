using System;
using System.IO;
using C4Sharp.Extensions;

namespace C4Sharp.Models.Plantuml
{
    /// <summary>
    /// PlantUML Resources
    /// </summary>    
    internal static class PlantumlResource
    {
        /// <summary>
        /// Load plantuml.jar as tempfile
        /// </summary>
        /// <returns>tempfile name</returns>
        /// <exception cref="PlantumlException"></exception>
        public static string Load()
        {
            try
            {
                var fileName = Path.GetTempFileName();

                using var resource = ResourceMethods.GetPlantumlResource();
                using var file = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                resource.CopyTo(file);

                return fileName;
            }
            catch (Exception e)
            {
                throw new PlantumlException($"{nameof(PlantumlException)}: Could not load plantuml engine.", e);
            }
        }

        /// <summary>
        /// Clear
        /// </summary>
        /// <param name="file"></param>
        public static void Clear(string file)
        {
            try
            {
                File.Delete(file);
            }
            catch
            {
                // ignored
            }
        }
    }
}