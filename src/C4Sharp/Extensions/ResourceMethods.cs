using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using C4Sharp.Models.Plantuml;

namespace C4Sharp.Extensions
{
    /// <summary>
    /// Methods to manipulate resource
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal static class ResourceMethods
    {
        /// <summary>
        /// Get Stream from plantuml.jar file
        /// </summary>
        /// <returns>Stream</returns>
        /// <exception cref="PlantumlException"></exception>
        public static Stream GetPlantumlResource() => GetResourceByName("C4Sharp.Resources.plantuml.jar");

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
                using var stream = GetResourceByName($"C4Sharp.Resources.{name}");
                using var reader = new StreamReader(stream ?? throw new InvalidOperationException());
                return reader.ReadToEnd();
            }
            catch (Exception e)
            {
                throw new PlantumlException($"{nameof(PlantumlException)}: Could not get resource.", e);
            }
        }

        /// <summary>
        /// Get resource stream content by resource name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="PlantumlException"></exception>
        private static Stream GetResourceByName(string name)
        {
            try
            {
                return Assembly
                    .GetExecutingAssembly()
                    .GetManifestResourceStream(name);
            }
            catch (Exception e)
            {
                throw new PlantumlException($"{nameof(PlantumlException)}: Could not get resource.", e);
            }
        } 
    }
}