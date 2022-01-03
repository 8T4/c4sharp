namespace C4Sharp.Diagrams.Core;

/// <summary>
/// The Component diagram shows how a container is made up of a number of "components", what each of those
/// components are, their responsibilities and the technology/implementation details.
/// <see href="https://c4model.com/#ComponentDiagram"/>
/// </summary>
public record ComponentDiagram() : Diagram(DiagramType.Component);
