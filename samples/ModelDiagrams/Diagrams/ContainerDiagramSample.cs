using C4Sharp.Diagrams.Builders;
using C4Sharp.Elements;
using C4Sharp.Elements.Relationships;
using static C4Sharp.Elements.ContainerType;

namespace ModelDiagrams.Diagrams;

public class ContainerDiagramSample : ContainerDiagram
{
    protected override string Title => "Container diagram for Internet Banking System v2";

    protected override IEnumerable<Structure> Structures =>
    [
        Person.None | Boundary.External | (
            alias: "Customer",
            label: "Personal Banking Customer",
            description: "A customer of the bank, with personal bank accounts."
        ),

        SoftwareSystem.None | (
            alias: "BankingSystem",
            label: "Internet Banking System",
            description: "Allows customers to view information about their bank accounts, and make payments."
        ),

        SoftwareSystem.None | Boundary.External | (
            alias: "MailSystem",
            label: "E-mail system",
            description: "The internal Microsoft Exchange e-mail system."
        ),

        Bound("c1", "Internet Banking",
            Container.Undefined | (
                type: WebApplication,
                alias: "WebApp",
                label: "WebApp",
                technology: "C#, WebApi",
                description: "Delivers the static content and the Internet banking SPA"
            ),
            Container.None | (
                type: Spa,
                alias: "Spa",
                label: "Spa",
                technology: "JavaScript, Angular",
                description: "Delivers the static content and the Internet banking SPA"
            ),
            Container.None | (
                type: Mobile,
                alias: "MobileApp",
                label: "Mobile App",
                technology: "C#, Xamarin",
                description: "Provides a mobile banking experience"
            ),
            Container.None | (
                type: Database,
                alias: "SqlDatabase",
                label: "SqlDatabase",
                technology: "SQL Database",
                description: "Stores user registration information, hashed auth credentials, access logs, etc."
            ),
            Container.None | (
                type: Queue,
                alias: "RabbitMQ",
                label: "RabbitMQ",
                technology: "RabbitMQ",
                description: "Stores user registration information, hashed auth credentials, access logs, etc."
            ),
            Container.None | (
                type: Api,
                alias: "BackendApi",
                label: "BackendApi",
                technology: "Dotnet, Docker Container",
                description: "Provides Internet banking functionality via API."
            )
        )
    ];

    protected override IEnumerable<Relationship> Relationships => new[]
    {
        this["Customer"] > this["WebApp"] | ("Uses", "HTTPS"),
        this["Customer"] > this["Spa"] | ("Uses", "HTTPS"),
        this["Customer"] > this["MobileApp"] | "Uses",

        this["WebApp"] > this["Spa"] | "Delivers" | Position.Neighbor,
        this["Spa"] > this["BackendApi"] | ("Uses", "async, JSON/HTTPS"),
        this["MobileApp"] > this["BackendApi"] | ("Uses", "async, JSON/HTTPS"),
        this["SqlDatabase"] < this["BackendApi"] | ("Uses", "async, JSON/HTTPS") | Position.Neighbor,
        this["RabbitMQ"] < this["BackendApi"] | ("Uses", "async, JSON"),

        this["Customer"] < this["MailSystem"] | "Sends e-mails to",
        this["MailSystem"] < this["BackendApi"] | ("Sends e-mails using", "sync, SMTP"),
        this["BackendApi"] > this["BankingSystem"] | ("Uses", "sync/async, XML/HTTPS") | Position.Neighbor
    };
}