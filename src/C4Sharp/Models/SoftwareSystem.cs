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
        private SoftwareSystemType SoftwareSystemType { get; }

        public SoftwareSystem(string alias, string label, string description, SoftwareSystemType softwareSystemType = SoftwareSystemType.Internal ) 
            : base(alias, label, description)
        {
            SoftwareSystemType = softwareSystemType;
        }
        
        public SoftwareSystem(string alias, string label,  SoftwareSystemType softwareSystemType = SoftwareSystemType.Internal ) 
            : base(alias, label)
        {
            SoftwareSystemType = softwareSystemType;
        }        

        public override string ToString()
        {
            return SoftwareSystemType == SoftwareSystemType.External
                ? $"System_Ext({Alias}, \"{Label}\", \"{Description}\")"
                : $"System({Alias}, \"{Label}\", \"{Description}\")";
        }
    }
    
    
}