using System.Collections.Generic;
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
        protected string Label { get; }
        protected string Description { get; }
        protected ICollection<string> Tags { get; }

        protected Structure(string alias, string label,  string description = default, ICollection<string> tags = default)
        {
            (Alias, Label, Description, Tags) = (alias, label, description, tags);
        }

        public static Relationship operator >(Structure a, Structure b) =>
            new Relationship(a, b, "uses");

        public static Relationship operator >=(Structure a, Structure b) =>
            new Relationship(a, Direction.Bidirectional, b, "uses");

        public static Relationship operator <=(Structure a, Structure b) =>
            new Relationship(a, Direction.Bidirectional, b, "uses");

        public static Relationship operator <(Structure a, Structure b) =>
            new Relationship(a, Direction.Back, b, "uses");
    }
}