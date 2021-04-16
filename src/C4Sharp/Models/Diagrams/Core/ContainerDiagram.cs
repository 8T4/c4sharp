namespace C4Sharp.Models.Diagrams.Core
{
    /// <summary>
    /// The Container diagram shows the high-level shape of the software architecture and how responsibilities are
    /// distributed across it. It also shows the major technology choices and how the containers communicate with one
    /// another. It's a simple, high-level technology focussed diagram that is useful for software developers and
    /// support/operations staff alike.
    /// <see href="https://c4model.com/#ContainerDiagram"/>
    /// </summary>    
    public class ContainerDiagram: Diagram
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ContainerDiagram() : base("C4_Container")
        {
        }
    }
}