using System;
using C4Sharp.Models.Relationships;

namespace C4Sharp.Models
{
    /// <summary>
    /// A software system is the highest level of abstraction and describes something that delivers value to its users,
    /// whether they are human or not. This includes the software system you are modelling, and the other software
    /// systems upon which your software system depends (or vice versa). In many cases, a software system is "owned by"
    /// a single software development team.
    /// <see href="https://c4model.com/"/>
    /// </summary>
    public sealed class SoftwareSystem : Structure
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should be unique</param>
        /// <param name="label"></param>
        public SoftwareSystem(string alias, string label)
            : base(alias, label, string.Empty, Boundary.Internal)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should be unique</param>
        /// <param name="label"></param>
        /// <param name="description"></param>
        public SoftwareSystem(string alias, string label, string description)
            : base(alias, label, description, Boundary.Internal)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should be unique</param>
        /// <param name="label"></param>
        /// <param name="description"></param>
        /// <param name="link"></param>
        public SoftwareSystem(string alias, string label, string description, string link)
            : base(alias, label, description, link, Boundary.Internal)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should be unique</param>
        /// <param name="label"></param>
        /// <param name="description"></param>
        /// <param name="boundary"></param>
        public SoftwareSystem(string alias, string label, string description, Boundary boundary) 
            : base(alias, label, description, boundary)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should be unique</param>
        /// <param name="label"></param>
        /// <param name="description"></param>
        /// <param name="link"></param>
        /// <param name="boundary"></param>
        public SoftwareSystem(string alias, string label, string description, string link, Boundary boundary)
            : base(alias, label, description, link, boundary)
        {
        }
    }
}