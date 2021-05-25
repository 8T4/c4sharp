using C4Sharp.Models.Relationships;

namespace C4Sharp.Models
{
    /// <summary>
    /// The word "component" is a hugely overloaded term in the software development industry, but in this context a
    /// component is a grouping of related functionality encapsulated behind a well-defined interface. If you're using
    /// a language like Java or C#, the simplest way to think of a component is that it's a collection of implementation
    /// classes behind an interface. Aspects such as how those components are packaged (e.g. one component vs many
    /// components per JAR file, DLL, shared library, etc) is a separate and orthogonal concern.
    /// An important point to note here is that all components inside a container typically execute in the same process
    /// space. In the C4 model, components are not separately deployable units.
    /// <see href="https://c4model.com/"/>
    /// </summary>
    public sealed class Component : Structure
    {
        public string Technology { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Unique identification</param>
        /// <param name="label"></param>
        /// <param name="description"></param>
        /// <param name="technology"></param>
        public Component(string alias, string label, string description, string technology) 
            : base(alias, label, description, Boundary.Internal)
        {
            Technology = technology;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Unique identification</param>
        /// <param name="label"></param>
        /// <param name="description"></param>
        /// <param name="technology"></param>
        /// <param name="link"></param>
        public Component(string alias, string label, string description, string technology, string link)
            : base(alias, label, description, link)
        {
            Technology = technology;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Unique identification</param>
        /// <param name="label"></param>
        /// <param name="description"></param>
        /// <param name="technology"></param>
        /// <param name="boundary"></param>
        public Component(string alias, string label, string description, string technology, Boundary boundary) 
            : base(alias, label, description, boundary)
        {
            Technology = technology;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Unique identification</param>
        /// <param name="label"></param>
        /// <param name="description"></param>
        /// <param name="technology"></param>
        /// <param name="link"></param>
        /// <param name="boundary"></param>
        public Component(string alias, string label, string description, string technology, string link, Boundary boundary)
            : base(alias, label, description, link, boundary)
        {
            Technology = technology;
        }
    }
}