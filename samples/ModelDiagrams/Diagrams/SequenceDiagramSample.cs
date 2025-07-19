using C4Sharp.Diagrams.Builders;
using C4Sharp.Elements;
using C4Sharp.Elements.Relationships;

namespace ModelDiagrams.Diagrams;

public class SequenceDiagramSample : SequenceDiagram
{
    protected override string Title => "Sequence diagram for Internet Banking System";

    protected override IEnumerable<Structure> Structures =>
    [
        new Container(
            alias: "cA",
            label: "Single-Page Application",
            type: ContainerType.None,
            technology: "JavaScript and Angular",
            description: "Provides all of the Internet banking functionality to customers via their web browser."
        ),

        Bound(alias: "b", label: "Api Application",
            new Component(
                alias: "cB",
                label: "Sign In Controller",
                type: ComponentType.None,
                technology: "Spring MVC Rest Controller",
                description: "Allows users to sign in to the Internet Banking System."
            ),
            new Component(
                alias: "cC",
                label: "Security Component",
                type: ComponentType.None,
                technology: "Spring Bean",
                description: "Provides functionality Related to signing in, changing passwords, etc."
            )
        ),

        new Container(
            alias: "cD",
            label: "Database",
            type: ContainerType.Database,
            technology: "Relational Database Schema",
            description: "Stores user registration information, hashed authentication credentials, access logs, etc."
        )
    ];

    protected override IEnumerable<Relationship> Relationships =>
    [
        It("cA") > It("cB") | ("Submits credentials to", "JSON/HTTPS"),
        It("cB") > It("cC") | "Calls isAuthenticated() on",
        It("cC") > It("cD") | ("select * from users where username = ?o", "JDBCS")
    ];
}