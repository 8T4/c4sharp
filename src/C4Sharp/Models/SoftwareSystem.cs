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
    public class SoftwareSystem : Structure
    {
        public SoftwareSystemType SoftwareSystemType { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should be unique</param>
        /// <param name="label"></param>
        public SoftwareSystem(string alias, string label) 
            : base(alias, label)
        {
            SoftwareSystemType = SoftwareSystemType.Internal;
        }             
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should be unique</param>
        /// <param name="label"></param>
        /// <param name="description"></param>
        public SoftwareSystem(string alias, string label, string description) 
            : base(alias, label, description)
        {
            SoftwareSystemType = SoftwareSystemType.Internal;
        }        
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should be unique</param>
        /// <param name="label"></param>
        /// <param name="description"></param>
        /// <param name="softwareSystemType"></param>
        [Obsolete("Uses Boundary enumeration insted SoftwareSystemType")]
        public SoftwareSystem(string alias, string label, string description, SoftwareSystemType softwareSystemType) 
            : base(alias, label, description)
        {
            SoftwareSystemType = softwareSystemType;
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should be unique</param>
        /// <param name="label"></param>
        /// <param name="description"></param>
        /// <param name="boundary"></param>
        public SoftwareSystem(string alias, string label, string description,  Boundary boundary) 
            : base(alias, label, description, boundary)
        {
        }        
    }
}