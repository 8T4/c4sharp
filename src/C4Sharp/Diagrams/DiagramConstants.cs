namespace C4Sharp.Diagrams;

public static class DiagramConstants
{
    public const string Component = "C4_Component";
    public const string Container = "C4_Container";
    public const string Context = "C4_Context";
    public const string Deployment = "C4_Deployment";
    
    public static string GetDescriptionOrDefault(this string description, DiagramType diagramType)
    {
        return !string.IsNullOrEmpty(description) ? description : diagramType.Value switch
        {
            Component => "The Component diagram shows how a container is made up of a number of components, what each of those components are, their responsibilities and the technology/implementation details.",
            Container => "A container is something like a server-side web application, single-page application, desktop application, mobile app, database schema, file system, etc. Essentially, a container is a separately runnable/deployable unit (e.g. a separate process space) that executes code or stores data.",
            Context => "A System Context diagram is a good starting point for diagramming and documenting a software system, allowing you to step back and see the big picture. Draw a diagram showing your system as a box in the centre, surrounded by its users and the other systems that it interacts with.",
            Deployment => "A deployment diagram allows you to illustrate how software systems and/or containers in the static model are mapped to infrastructure. This deployment diagram is based upon a UML deployment diagram, although simplified slightly to show the mapping between containers and deployment nodes.",
            _ => description
        };        
    }    
}