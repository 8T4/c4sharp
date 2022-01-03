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
    
    public class EnterpriseDiagramBuildRunner: DiagramBuildRunner
    {
        protected override string Title => "System Enterprise diagram for Internet Banking System";
        protected override DiagramType DiagramType => DiagramType.Context;

        protected override IEnumerable<Structure> Structures() => new Structure[]
        {
            Customer,
            EnterpriseBoundary(),
            Mainframe,
            MailSystem
        };

        private static EnterpriseBoundary EnterpriseBoundary()
        {
            return new EnterpriseBoundary("eboundary", "Domain A")
            {
                Structures = new Structure[]
                {
                    BankingSystem,
                    new EnterpriseBoundary("eboundary1", "Domain Internal Users")
                    {
                        Structures = new Structure[]
                        {
                            InternalCustomer,
                        }
                    },
                    new EnterpriseBoundary("eboundary2", "Domain Managers")
                    {
                        Structures = new Structure[]
                        {
                            Manager,
                        }
                    },
                }
            };
        }

        protected override IEnumerable<Relationship> Relationships() => new[]
        {
            Customer > BankingSystem,
            InternalCustomer > BankingSystem,
            Manager > BankingSystem,
            (Customer < MailSystem)["Sends e-mails to"],
            (BankingSystem > MailSystem)["Sends e-mails", "SMTP"][Neighbor],
            BankingSystem > Mainframe,
        };
    }
}