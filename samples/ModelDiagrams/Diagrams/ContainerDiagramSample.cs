using C4Sharp.Diagrams.Builders;
using C4Sharp.Elements;
using C4Sharp.Elements.Relationships;
using static C4Sharp.Elements.ContainerType;

namespace ModelDiagrams.Diagrams;

public class ContainerDiagramSample : ContainerDiagram
{
    protected override string Title => "Container diagram for Internet Banking System v2";

    protected override IEnumerable<Structure> Structures => new Structure[]
    {
        Person.None | Boundary.External 
                    | ("Customer", "Personal Banking Customer", "A customer of the bank, with personal bank accounts."),
        
        SoftwareSystem.None | ("BankingSystem", "Internet Banking System", 
            "Allows customers to view information about their bank accounts, and make payments."),
        
        SoftwareSystem.None | Boundary.External 
                            | ("MailSystem", "E-mail system", "The internal Microsoft Exchange e-mail system."),
        
        Bound("c1", "Internet Banking",
            Container.None | (WebApplication, "WebApp", "WebApp", "C#, WebApi", 
                "Delivers the static content and the Internet banking SPA"),
            
            Container.None | (Spa, "Spa", "Spa", "JavaScript, Angular", 
                "Delivers the static content and the Internet banking SPA"),
            
            Container.None | (Mobile, "MobileApp", "Mobile App", "C#, Xamarin", 
                "Provides a mobile banking experience"),
            
            Container.None | (Database, "SqlDatabase", "SqlDatabase", "SQL Database", 
                "Stores user registration information, hashed auth credentials, access logs, etc."),   
            
            Container.None | (Queue, "RabbitMQ", "RabbitMQ", "RabbitMQ", 
                "Stores user registration information, hashed auth credentials, access logs, etc."),
            
            Container.None | (Api, "BackendApi", "BackendApi", "Dotnet, Docker Container", 
                "Provides Internet banking functionality via API.")
        )
    };

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