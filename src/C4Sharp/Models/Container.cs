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
    public class Container : Structure
    {
        private readonly Dictionary<int, Container> _instances = 
            new Dictionary<int, Container>();

        public string Technology { get; }
        public ContainerType ContainerType { get; }
        public Container this[int index] => this.NewInstance(index);
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should  be unique</param>
        /// <param name="type"></param>
        /// <param name="description"></param>
        /// <param name="technology"></param>
        public Container(string alias, ContainerType type, string description, string technology) 
            : base(alias, type.GetDescription(), description)
        {
            Technology = technology;
            ContainerType = type;
        }    
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should  be unique</param>
        /// <param name="type"></param>
        /// <param name="description"></param>
        /// <param name="technology"></param>
        /// <param name="boundary"></param>
        public Container(string alias, ContainerType type, string description, string technology, Boundary boundary) 
            : base(alias, type.GetDescription(), description, boundary)
        {
            Technology = technology;
            ContainerType = type;
        }          
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should  be unique</param>
        /// <param name="label"></param>
        /// <param name="description"></param>
        /// <param name="technology"></param>
        public Container(string alias, string label, string description, string technology) 
            : base(alias, label, description)
        {
            Technology = technology;
            ContainerType = ContainerType.None;
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should  be unique</param>
        /// <param name="label"></param>
        /// <param name="description"></param>
        /// <param name="technology"></param>
        /// <param name="boundary"></param>
        public Container(string alias, string label, string description, string technology, Boundary boundary) 
            : base(alias, label, description, boundary)
        {
            Technology = technology;
            ContainerType = ContainerType.None;
        }

        /// <summary>
        /// Create a new instance of current container
        /// </summary>
        /// <param name="code">instance code</param>
        /// <returns>New Container</returns>
        private Container NewInstance(int code)
        {
            if (_instances.ContainsKey(code))
            {
                return _instances[code];
            }

            var container = new Container($"{Alias}{code}", ContainerType, Description, Technology, Boundary);
            _instances[code] = container;
            return container;
        }
    }
}