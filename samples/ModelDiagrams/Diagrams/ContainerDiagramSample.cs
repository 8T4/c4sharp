using C4Sharp.Diagrams.Builders;
using C4Sharp.Elements;
using C4Sharp.Elements.Relationships;
using ModelDiagrams.Structures;
using static C4Sharp.Elements.ContainerType;
using static ModelDiagrams.Structures.Systems;
using static ModelDiagrams.Structures.Containers;
using static ModelDiagrams.Structures.People;

namespace ModelDiagrams.Diagrams;

public class ContainerDiagramSample : ContainerDiagram
{
    protected override string Title => "Container diagram for Internet Banking System";

    protected override IEnumerable<Structure> Structures => new Structure[]
    {
        Person.None | Boundary.External 
                    | ("customer", "Personal Banking Customer", "A customer of the bank, with personal bank accounts."),
        
        SoftwareSystem.None | ("BankingSystem", "Internet Banking System", 
            "Allows customers to view information about their bank accounts, and make payments."),
        
        SoftwareSystem.None | Boundary.External 
                            | ("MailSystem", "E-mail system", "The internal Microsoft Exchange e-mail system."),
        
        Bound("c1", "Internet Banking",
            Container.None | (WebApplication, "Corporate.Finance.Limits.Service.ServiceBus", "WebApp", "C#, WebApi", 
                "Delivers the static content and the Internet banking SPA"),
            
            Container.None | (Spa, "Spa", "Spa", "JavaScript, Angular", 
                "Delivers the static content and the Internet banking SPA"),
            
            MobileApp,
            SqlDatabase,
            RabbitMq,
            BackendApi
        )
    };

    protected override IEnumerable<Relationship> Relationships => new[]
    {
        Customer > WebApp | ("Uses", "HTTPS"),
        Customer > Containers.SpaApp | ("Uses", "HTTPS"),
        Customer > MobileApp | "Uses",

        WebApp > Containers.SpaApp | "Delivers" | Position.Neighbor,
        Containers.SpaApp > BackendApi | ("Uses", "async, JSON/HTTPS"),
        MobileApp > BackendApi | ("Uses", "async, JSON/HTTPS"),
        SqlDatabase < BackendApi | ("Uses", "async, JSON/HTTPS") | Position.Neighbor,
        RabbitMq < BackendApi | ("Uses", "async, JSON"),

        Customer < MailSystem | "Sends e-mails to",
        MailSystem < BackendApi | ("Sends e-mails using", "sync, SMTP"),
        BackendApi > BankingSystem | ("Uses", "sync/async, XML/HTTPS") | Position.Neighbor
    };
}