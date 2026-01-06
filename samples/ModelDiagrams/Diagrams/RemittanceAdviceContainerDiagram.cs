using C4Sharp.Diagrams.Builders;
using C4Sharp.Elements;
using C4Sharp.Elements.Containers;
using C4Sharp.Elements.Relationships;
using static C4Sharp.Elements.ContainerType;

using ModelDiagrams.Structures;

namespace ModelDiagrams.Diagrams;

using static People;
using static Systems;
using static C4Sharp.Elements.Relationships.Position;

    public class RemittanceAdviceContainerDiagram : ContainerDiagram
    {
        protected override string Title => "Pharmacy Direct - Existing Container Diagram";

        protected override IEnumerable<Structure> Structures => new Structure[]
        {
            // External Person
            Person.None | Boundary.External
                | ("FinanceTeam", "Finance Team (Manual)", "Human actors verifying/approving exceptions"),
            
            // External Software Systems
            SoftwareSystem.None | Boundary.External
                | ("MedicalAidSchemes", "Medical Aid Schemes", "External providers of ERA files"),

            SoftwareSystem.None | Boundary.External
                | ("Bank", "Banking System", "Provides Bank CSV/XLS"),

            SoftwareSystem.None | Boundary.External
                | ("SFTP", "BI Solutions / Allegra SFTP", "ERA files are placed here by Allegra/BI Solutions"),
            
            // Pharmacy Direct Environment (Boundary)
            Bound("pd", "Pharmacy Direct System",
                // Containers inside PD
                Container.None | (ContainerType.Api, "PowerAutomateWorker", "Power Automate + PD Worker Service", "Fetch/import CSV/XLS or ERA files from SharePoint/SFTP into SQL"),
                Container.None | (ContainerType.Database, "SqlDataWarehouse", "SQL Data Warehouse", "Stores ERA lines, Bank lines, claims, etc."),
                Container.None | (ContainerType.Api, "RemittanceApiGateway", "Remittance API Gateway", "Receives consolidated or partial payloads, publishes to queue"),
                Container.None | (ContainerType.Queue, "RabbitQueue", "RabbitMQ Queue", "Broker for remittance messages to the processor"),
                Container.None | (ContainerType.Api, "RemittanceProcessor", "Remittance Processor Service", "Consumes messages, updates claims/payment tables"),
                Container.None | (ContainerType.WebApplication, "AllegraSystem", "Allegra Debtors System", "Debtors/Claims system used by PD"),
                Container.None | (ContainerType.WebApplication, "PastelAccounting", "Pastel Accounting System", "Used by Finance for GL exports"),
                Container.None | (ContainerType.WebApplication, "PowerBITracker", "Remittance Advice Tracker (Power BI)", "BI dashboards & real-time insights")
            )
        };

        protected override IEnumerable<Relationship> Relationships => new[]
        {
            // External interactions
            this["MedicalAidSchemes"] > this["SFTP"] | ("Provide ERA files", "SFTP"),
            this["Bank"] > this["PowerAutomateWorker"] | ("Send Bank CSV/XLS", "SharePoint/Automate"),
            
            // Worker -> SQL
            this["SFTP"] > this["PowerAutomateWorker"] | "ERA .csv/.txt for PD Worker to process",
            this["PowerAutomateWorker"] > this["SqlDataWarehouse"] | "Parse & load ERA & Bank data",
            
            // Internal flows
            this["SqlDataWarehouse"] > this["AllegraSystem"] | "Partially processed lines",
            this["AllegraSystem"] > this["FinanceTeam"] | "Manual exception review",
            this["FinanceTeam"] > this["AllegraSystem"] | "Approve/adjust allocations",
            
            // API & Messaging
            this["RemittanceApiGateway"] > this["RabbitQueue"] | "Publish incoming payload",
            this["RabbitQueue"] > this["RemittanceProcessor"] | "Consume remittance messages",
            this["RemittanceProcessor"] > this["SqlDataWarehouse"] | "Update claims/payment tables",
            this["RemittanceProcessor"] > this["AllegraSystem"] | "Apply reconciliations",
            
            // BI & Exports
            this["SqlDataWarehouse"] > this["PowerBITracker"] | "Data source (Remittance Advice Tracker)",
            this["AllegraSystem"] > this["PastelAccounting"] | "Export to GL",
            this["PastelAccounting"] > this["PowerAutomateWorker"] | "Uploads Pastel CSV to PD environment",
            this["PowerAutomateWorker"] > this["SqlDataWarehouse"] | "Import Pastel data",
            this["PowerBITracker"] > this["FinanceTeam"] | "Dashboards & insights"
        };
    }


