using C4Sharp.Models.Relationships;

namespace C4Sharp.Models
{
    /// <summary>
    /// A person represents one of the human users of your software system (e.g. actors, roles, personas, etc)
    /// <see href="https://c4model.com/"/>
    /// </summary>
    public sealed class Person : Structure
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should be unique</param>
        /// <param name="label"></param>
        /// <param name="description"></param>
        public Person(string alias, string label, string description) 
            : base(alias, label, description)
        {
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should be unique</param>
        /// <param name="label"></param>
        /// <param name="description"></param>
        /// <param name="boundary"></param>
        public Person(string alias, string label, string description, Boundary boundary) 
            : base(alias, label, description, boundary)
        {
        }        
    }
}