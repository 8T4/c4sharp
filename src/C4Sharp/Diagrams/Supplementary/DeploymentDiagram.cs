namespace C4Sharp.Diagrams.Supplementary;

/// <summary>
/// A deployment diagram allows you to illustrate how software systems and/or containers in the static model are
/// mapped to infrastructure. This deployment diagram is based upon a UML deployment diagram, although simplified
/// slightly to show the mapping between containers and deployment nodes. A deployment node is something like
/// physical infrastructure (e.g. a physical server or device), virtualised infrastructure (e.g. IaaS, PaaS,
/// a virtual machine), containerised infrastructure (e.g. a Docker container), an execution environment
/// (e.g. a database server, Java EE web/application server, Microsoft IIS), etc. Deployment nodes can be nested.
/// <see href="https://c4model.com/#DeploymentDiagram"/>
/// </summary>
public record DeploymentDiagram() : Diagram(DiagramType.Deployment);
