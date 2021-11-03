using System.Collections.Generic;
using C4Sharp.Extensions;
using C4Sharp.Models.Relationships;

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
        private readonly Dictionary<int, Container> _instances = new();
        public ContainerType ContainerType{ get; init; }
        public string Technology { get; init; }
        public Container this[int index] => this.GetInstance(index);

        /// <summary>
        /// Get or Create a instance of current container
        /// </summary>
        /// <param name="code">instance code</param>
        /// <returns>New Container</returns>
        private Container GetInstance(int code)
        {
            if (_instances.ContainsKey(code))
            {
                return _instances[code];
            }

            var container = new Container($"{Alias}{code}", Label) with
            {
                ContainerType = this.ContainerType, 
                Description = this.Description, 
                Technology = this.Technology,
                Boundary = this.Boundary
            };

            _instances[code] = container;
            return container;
        }
    }
}