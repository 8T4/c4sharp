using C4Sharp.Elements;

namespace C4Sharp.Diagrams.Builders;

public abstract class DeploymentDiagram : DiagramBuilder
{
    protected override string Title => "Deployment Diagram";
    protected override DiagramType DiagramType { get; } = DiagramType.Deployment;

    protected static DeploymentNode Node(string alias, string label, params DeploymentNode[] nodes) =>
        new(alias, label, nodes);

    protected static DeploymentNode Node(string alias, string label, string description, params DeploymentNode[] nodes) =>
        new(alias, label, nodes) { Description = description };
}