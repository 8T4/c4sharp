using C4Sharp.Diagrams.Builders;
using C4Sharp.Elements;
using C4Sharp.Elements.Relationships;

namespace ModelDiagrams.Diagrams;

public class SequenceDiagramSample : SequenceDiagram
{
    protected override string Title => "Sequence diagram for Internet Banking System";

    protected override IEnumerable<Structure> Structures => new Structure[]
    {
        new Container("cA", "Single-Page Application", ContainerType.None, "JavaScript and Angular",
            "Provides all of the Internet banking functionality to customers via their web browser."),

        Bound("b", "Api Application",
            new("cB", "Sign In Controller", ComponentType.None, "Spring MVC Rest Controller",
                "Allows users to sign in to the Internet Banking System."),
            new("cC", "Security Component", ComponentType.None, "Spring Bean",
                "Provides functionality Related to signing in, changing passwords, etc.")
        ),
        
        new Container("cD", "Database", ContainerType.Database, "Relational Database Schema",
            "Stores user registration information, hashed authentication credentials, access logs, etc.")
    };

    protected override IEnumerable<Relationship> Relationships => new[]
    {
        It("cA") > It("cB") | ("Submits credentials to", "JSON/HTTPS"),
        It("cB") > It("cC") | "Calls isAuthenticated() on",
        It("cC") > It("cD") | ("select * from users where username = ?o", "JDBCS")
    };
}