using System.Collections.Generic;
using C4Sharp.Extensions;

namespace C4Sharp.Models
{
    /// <summary>
    /// Not Docker! In the C4 model, a container represents an application or a data store. A container is something
    /// that needs to be running in order for the overall software system to work. In real terms, a container is
    /// something like:
    ///
    /// Server-side web application, Client-side web application, Client-side desktop application,
    /// Mobile app, Server-side console application, Serverless function, Database, Blob or content store,
    /// File system, Shell script
    ///
    /// <see href="https://c4model.com/#ContainerDiagram"/>
    /// </summary>
    public sealed record Container(string Alias, string Label) : Structure(Alias, Label)
    {
        private readonly Dictionary<string, Container> _instances = new();
        
        public ContainerType ContainerType{ get; init; }
        public string? Technology { get; init; }
        public Container this[int index] => GetInstance($"instance {index}");
        public Container this[string instanceName] => GetInstance(instanceName);

        /// <summary>
        /// Get or Create a instance of current container
        /// </summary>
        /// <param name="name">instance name</param>
        /// <returns>New Container</returns>
        private Container GetInstance(string name)
        {
            var key = name.GenerateSlug(".");
            
            if (_instances.ContainsKey(key))
            {
                return _instances[key];
            }
            
            var container = new Container($"{Alias}.{key}", Label)
            {
                ContainerType = ContainerType, 
                Description = Description, 
                Technology = Technology,
                Boundary = Boundary
            };

            _instances[key] = container;
            return container;
        }
    }
}