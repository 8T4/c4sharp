using System;
using System.IO;
using System.Reflection;
using C4Sharp.Extensions;
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
            try
            {
                var fileName = Path.GetTempFileName();

                using (var resource = ResourceMethods.GetPlantumlResource())
                {
                    using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                    {
                        resource.CopyTo(file);
                    }
                }

                return fileName;
            }
            catch (Exception e)
            {
                throw new PlantumlException($"{nameof(PlantumlException)}: Could not load plantuml engine.", e);
            }
        }

        public static void Clear(string file)
        {
            try
            {
                File.Delete(file);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}