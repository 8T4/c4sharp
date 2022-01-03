namespace C4Sharp.Models;

/// <summary>
/// A deployment node is something like physical infrastructure (e.g. a physical server or device),
/// virtualised infrastructure (e.g. IaaS, PaaS, a virtual machine), containerised infrastructure
/// (e.g. a Docker container), an execution environment (e.g. a database server, Java EE web/application
/// server, Microsoft IIS), etc. Deployment nodes can be nested.
/// <see href="https://c4model.com/#DeploymentDiagram"/>
/// </summary>
public sealed record DeploymentNode(string Alias, string Label) : Structure(Alias, Label)
{
    public IEnumerable<DeploymentNode> Nodes { get; init; } = Array.Empty<DeploymentNode>();
    public Dictionary<string, string> Properties { get; init; } = new();
    public Container? Container { get; init; }
}
