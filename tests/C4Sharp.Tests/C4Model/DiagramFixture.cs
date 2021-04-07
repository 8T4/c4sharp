using System.IO;
using C4Sharp.Models;
using C4Sharp.Models.Diagrams.Core;
using C4Sharp.Models.Diagrams.Supplementary;
using C4Sharp.Models.Plantuml;
using C4Sharp.Models.Relationships;
using Xunit;

namespace C4Sharp.Tests.C4Model
{
    using static Position;
    using static Persons;
    using static Systems;
    using static Containers;
    using static Components;
    using static Nodes;    
    
    public static class DiagramFixture
    {
        public static ContextDiagram BuildContextDiagram()
        {
            var diagram = new ContextDiagram
            {
                Title = "System Context diagram for Internet Banking System",
                Structures = new Structure[]
                {
                    //Customer,
                    BankingSystem,
                    Mainframe,
                    MailSystem
                },
                Relationships = new[]
                {
                    Customer > BankingSystem,
                    (Customer < MailSystem)["Sends e-mails to"],
                    (BankingSystem > MailSystem)["Sends e-mails", "SMTP"][Neighbor],
                    BankingSystem > Mainframe,
                }
            };

            return diagram;
        }

        public static ContainerDiagram BuildContainerDiagram()
        {
            var boundary = new SoftwareSystemBoundary("c1", "Internet Banking")
            {
                Containers = new[]
                {
                    WebApp,
                    Spa,
                    MobileApp,
                    SqlDatabase,
                    BackendApi
                }
            };

            return new ContainerDiagram
            {
                Title = "Container diagram for Internet Banking System",
                Structures = new Structure[]
                {
                    Customer,
                    boundary,
                    BankingSystem,
                    MailSystem,
                },
                Relationships = new[]
                {
                    (Customer > WebApp)["Uses", "HTTPS"],
                    (Customer > Spa)["Uses", "HTTPS"],
                    (Customer > MobileApp)["Uses"],

                    (WebApp > Spa)["Delivers"][Neighbor],
                    (Spa > BackendApi)["Uses", "async, JSON/HTTPS"],
                    (MobileApp > BackendApi)["Uses", "async, JSON/HTTPS"],
                    (SqlDatabase < BackendApi)["Uses", "async, JSON/HTTPS"][Neighbor],

                    (Customer < MailSystem)["Sends e-mails to"],
                    (MailSystem < BackendApi)["Sends e-mails using", "sync, SMTP"],
                    (BackendApi > BankingSystem)["Uses", "sync/async, XML/HTTPS"][Neighbor]
                }
            };
        }
        
        public static ComponentDiagram BuildComponentDiagram()
        {
            var boundary = new ContainerBoundary("c1", "API Application")
            { 
                Components = new[]
                {
                    Sign,
                    Accounts,
                    Security,
                    MainframeFacade
                },
                Relationships = new []
                {
                    Sign > Security,
                    Accounts > MainframeFacade,
                    (Security > SqlDatabase) ["Read & write to", "JDBC"],
                    (MainframeFacade > Mainframe)["Uses", "XML/HTTPS"]
                }
            };

            return new ComponentDiagram
            {
                Title = "Internet Banking System API Application",
                Structures = new Structure[]
                {
                    Spa,
                    MobileApp,
                    SqlDatabase,
                    Mainframe,
                    boundary,
                },
                Relationships = new[]
                {
                    (Spa > Sign)["Uses", "JSON/HTTPS"],
                    (Spa > Accounts)["Uses", "JSON/HTTPS"],
                    (MobileApp > Sign)["Uses", "JSON/HTTPS"],
                    (MobileApp > Accounts)["Uses", "JSON/HTTPS"],
                }
            };
        }      
        
        public static DeploymentDiagram BuildDeploymentDiagram()
        {
            var db1 = OracleDatabaseInstance("db01");
            var db2 = OracleDatabaseInstance("db02");
            
            var bigBankPlc = new DeploymentNode("plc", "Big Bank plc", "Big Bank plc data center")
            {
                Nodes = new[]
                {
                    Ubuntu("dn", "bigbank-api***\tx8", ApacheTomCat("apache", BackendApi)),
                    Ubuntu("bigbankdb01", "bigbank-db01", OracleNode("oracle", db1)),
                    Ubuntu("bigbankdb02", "bigbank-db02", OracleNode("oracle2", db2)),
                    Ubuntu("bb2", "bigbank-web***\tx4", ApacheTomCat("apache2", WebApp)),
                }
            };

            return new DeploymentDiagram
            {
                Title = "System Context diagram for Internet Banking System",
                Structures = new Structure[]
                {
                    bigBankPlc,
                    Ios("ios", MobileApp),
                    PersonalComputer("computer", Browser("browser", Spa))
                },
                Relationships = new[]
                {
                    (MobileApp > BackendApi)["Makes API calls to", "json/HTTPS"],
                    (Spa > BackendApi)["Makes API calls to", "json/HTTPS"],
                    (WebApp > Spa)["Delivers to the customer's web browser"][Up],
                    (BackendApi > db1)["Writes to", "JDBC"],
                    (BackendApi < db2)["Reads from", "JDBC"],
                    (db1 > db2)["Replicates data to", "JDBC"][Right],
                }
            };
        }        
    }
}