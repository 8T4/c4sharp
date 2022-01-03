namespace C4Sharp.Diagrams.Core;

/// <summary>
/// A System Context diagram is a good starting point for diagramming and documenting a software system, allowing
/// you to step back and see the big picture. Draw a diagram showing your system as a box in the centre, surrounded
/// by its users and the other systems that it interacts with.
/// <see href="https://c4model.com/#SystemContextDiagram"/>
/// </summary>
public record ContextDiagram() : Diagram(DiagramType.Context);
