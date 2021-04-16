using System;
using System.IO;
using System.Reflection;
using C4Sharp.Models.Plantuml;

namespace C4Sharp.Extensions
{
    /// <summary>
    /// Methods to manipulate resource
    /// </summary>
    internal static class ResourceMethods
    {
        /// <summary>
        /// Get Stream from plantuml.jar file
        /// </summary>
        /// <returns>Stream</returns>
        /// <exception cref="PlantumlException"></exception>
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
        
        /// <summary>
        /// Get resource string content from resource file
        /// </summary>
        /// <param name="name">file name</param>
        /// <returns>resource content</returns>
        /// <exception cref="PlantumlException"></exception>
        public static string GetResource(string name)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = $"C4Sharp.bin.{name}";

                using var stream = assembly.GetManifestResourceStream(resourceName);
                using var reader = new StreamReader(stream ?? throw new InvalidOperationException());
                return reader.ReadToEnd();
            }
            catch (Exception e)
            {
                throw new PlantumlException($"{nameof(PlantumlException)}: Could not get resource.", e);
            }
        }            
    }
}