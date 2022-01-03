using C4Sharp.Diagrams;
using C4Sharp.Diagrams.Core;
using C4Sharp.Models;
using C4Sharp.Models.Relationships;
using C4Sharp.Sample.Structures;

namespace C4Sharp.Sample.Diagrams
{
    using static Position;
    using static People;
    using static Systems;

    public class ContextDiagramBuildRunner: DiagramBuildRunner
    {
        protected override string Title => "System Context diagram for Internet Banking System";
        protected override DiagramType DiagramType => DiagramType.Context;

        protected override IEnumerable<Structure> Structures() => new Structure[]
        {
            Customer,
            BankingSystem,
            Mainframe,
            MailSystem
        };

        protected override IEnumerable<Relationship> Relationships() => new[]
        {
            (Customer > BankingSystem).AddTags("error"),
            (Customer < MailSystem)["Sends e-mails to"],
            (BankingSystem > MailSystem)["Sends e-mails", "SMTP"][Neighbor],
            BankingSystem > Mainframe,
        };
    }
}