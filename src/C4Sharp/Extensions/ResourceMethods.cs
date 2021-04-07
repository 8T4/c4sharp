using System;
using System.IO;
using System.Reflection;
using C4Sharp.Models.Plantuml;

namespace C4Sharp.Extensions
{
    internal static class ResourceMethods
    {
        public static Stream GetPlantumlResource()
        {
            try
            {
                return Assembly
                    .GetExecutingAssembly()
                    .GetManifestResourceStream($"C4Sharp.bin.plantuml.jar");
            }
            catch (Exception e)
            {
                throw new PlantumlException($"{nameof(PlantumlException)}: Could not get resource.", e);
            }
        }        
        
        public static string GetResource(string name)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = $"C4Sharp.bin.{name}";

                using var stream = assembly.GetManifestResourceStream(resourceName);
                using var reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
            catch (Exception e)
            {
                throw new PlantumlException($"{nameof(PlantumlException)}: Could not get resource.", e);
            }
        }            
    }
}