namespace C4Sharp.Models
{
    /// <summary>
    /// A person represents one of the human users of your software system (e.g. actors, roles, personas, etc)
    /// <see href="https://c4model.com/"/>
    /// </summary>
    public class Person : Structure
    {
        public Person(string alias, string label, string description) : base(alias, label, description)
        {
        }

        public override string ToString()
        {
            return $"Person({Alias}, \"{Label}\", \"{Description}\" )";
        }
    }
}