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
    public class Container : Structure
    {
        public string Technology { get; }
        public ContainerType ContainerType { get; }
        
        public Container(string alias, ContainerType type, string description, string technology) 
            : base(alias, type.GetDescription(), description)
        {
            Technology = technology;
            ContainerType = type;
        }        
        
        public Container(string alias, string label, string description, string technology) 
            : base(alias, label, description)
        {
            Technology = technology;
            ContainerType = ContainerType.None;
        }
    }
}