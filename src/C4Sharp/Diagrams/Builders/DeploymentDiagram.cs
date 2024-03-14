using C4Sharp.Elements;

namespace C4Sharp.Diagrams.Builders;

public abstract class DeploymentDiagram: DiagramBuilder
{
    protected override string Title { get; } = "Deployment Diagram";
    protected override DiagramType DiagramType { get; } = DiagramType.Deployment;
    
    protected DeploymentNode Node(string alias, string label, params DeploymentNode[] nodes) =>
        new(alias, label, nodes);
    
    protected DeploymentNode Node(string alias, string label, string description, params DeploymentNode[] nodes) =>
        new(alias, label, nodes) {Description = description};    
}