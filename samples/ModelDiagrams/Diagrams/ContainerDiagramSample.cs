using C4Sharp.Diagrams.Builders;
using C4Sharp.Elements;
using C4Sharp.Elements.Relationships;

using static ModelDiagrams.Structures.Systems;
using static ModelDiagrams.Structures.Containers;
using static ModelDiagrams.Structures.People;

namespace ModelDiagrams.Diagrams;

public class ContainerDiagramSample: ContainerDiagram
{
    protected override string Title => "Container diagram for Internet Banking System";

    protected override IEnumerable<Structure> Structures => new Structure[]
    {
        Customer,
        BankingSystem,
        MailSystem,
        Boundary("c1", "Internet Banking",
            WebApp,
            Spa,
            MobileApp,
            SqlDatabase,
            RabbitMq,
            BackendApi
        )
    };

    protected override IEnumerable<Relationship> Relationships => new[]
    {
        Customer > WebApp | ("Uses", "HTTPS"),
        Customer > Spa | ("Uses", "HTTPS"),
        Customer > MobileApp | "Uses",

        WebApp > Spa | "Delivers" | Position.Neighbor,
        Spa > BackendApi | ("Uses", "async, JSON/HTTPS"),
        MobileApp > BackendApi | ("Uses", "async, JSON/HTTPS"),
        SqlDatabase < BackendApi | ("Uses", "async, JSON/HTTPS") | Position.Neighbor,
        RabbitMq < BackendApi | ("Uses", "async, JSON"),

        Customer < MailSystem | "Sends e-mails to",
        MailSystem < BackendApi | ("Sends e-mails using", "sync, SMTP"),
        BackendApi > BankingSystem | ("Uses", "sync/async, XML/HTTPS") | Position.Neighbor
    };
}