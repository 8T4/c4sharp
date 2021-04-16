using C4Sharp.Models.Relationships;

namespace C4Sharp.Models
{
    /// <summary>
    /// In order to create these maps of your code, we first need a common set of abstractions to create a ubiquitous
    /// language that we can use to describe the static structure of a software system. The C4 model considers the
    /// static structures of a software system in terms of containers, components and code. And people use the software
    /// systems that we build.
    /// <see href="https://c4model.com/"/>
    /// </summary>
    public abstract class Structure
    {
        public string Alias { get; }
        public string Label { get; }
        public string Description { get; }
        public string[] Tags { get; private set; }
        public Boundary Boundary { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should be unique</param>
        /// <param name="label"></param>
        protected Structure(string alias, string label) =>
            (Alias, Label, Description, Boundary) = (alias, label, string.Empty, Boundary.Internal);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should be unique</param>
        /// <param name="label"></param>
        /// <param name="description"></param>
        protected Structure(string alias, string label, string description) =>
            (Alias, Label, Description, Boundary) = (alias, label, description, Boundary.Internal);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should be unique</param>
        /// <param name="label"></param>
        /// <param name="description"></param>
        /// <param name="boundary"></param>
        protected Structure(string alias, string label, string description, Boundary boundary) =>
            (Alias, Label, Description, Boundary) = (alias, label, description, boundary);

        /// <summary>
        /// Add Tags
        /// </summary>
        /// <param name="tags"></param>
        public void AddTag(params string[] tags) => Tags = tags;

        /// <summary>
        /// Forward relationship
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Relationship operator >(Structure a, Structure b) =>
            new Relationship(a, b, "uses");

        /// <summary>
        /// Bidirectional relationship
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Relationship operator >=(Structure a, Structure b) =>
            new Relationship(a, Direction.Bidirectional, b, "uses");

        /// <summary>
        /// Bidirectional relationship
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Relationship operator <=(Structure a, Structure b) =>
            new Relationship(a, Direction.Bidirectional, b, "uses");

        /// <summary>
        /// Back relationship
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Relationship operator <(Structure a, Structure b) =>
            new Relationship(a, Direction.Back, b, "uses");
    }
}