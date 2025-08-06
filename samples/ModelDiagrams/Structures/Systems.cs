using C4Sharp.Elements;
using C4Sharp.Elements.Relationships;

namespace ModelDiagrams.Structures;

public static class Systems
{
    public static SoftwareSystem MedicalAidSchemes = new SoftwareSystem(
            "Medical Aid Schemes",
            "External providers of ERA files");

    public static SoftwareSystem BankingSystem = new SoftwareSystem(
        "Banking System",
        "Provides bank statements in CSV/XLS");

    public static SoftwareSystem AllegraSftp = new SoftwareSystem(
        "BI Solutions / Allegra SFTP",
        "SFTP location where ERA files are delivered");

    public static SoftwareSystem MediMatchSystem = new SoftwareSystem(
        "MediMatch Middleware",
        "Reconciliation engine and APIs");
   public static SoftwareSystem PharmacyDirectSystem =>
        new("PharmacyDirectSystem", "Pharmacy Direct System",
            "Handles bank/ERA ingestion, claims, accounting, and reporting.", Boundary.External);
    public static SoftwareSystem PowerAutomate =>
        new("PowerAutomate", "Power Automate + PD Worker Service",
            "Fetches/imports CSV/XLS or ERA files from SharePoint/SFTP into SQL.", Boundary.External);
    public static SoftwareSystem BankingSystem => new(
        alias: "BankingSystem",
        label: "Internet Banking System",
        description: "Allows customers to view information about their bank accounts, and make payments."
    );

    public static SoftwareSystem SqlDataWarehouse =>
        new("SqlDataWarehouse", "SQL Data Warehouse",
            "Central PD data store for bank, ERA, claims, etc.", Boundary.External);
    public static SoftwareSystem RemittanceApiGateway =>
        new("RemittanceApiGateway", "Remittance API Gateway",
            "Receives consolidated or age-analysis reports from MediMatch, publishes to RabbitMQ.", Boundary.External);

    public static SoftwareSystem RabbitMqQueue =>
        new("RabbitMqQueue", "RabbitMQ Queue",
            "Message broker to feed remittance data to the Processor service.", Boundary.External);

    public static SoftwareSystem RemittanceProcessor =>
        new("RemittanceProcessor", "Remittance Processor Service",
            "Applies reconciliation logic, updates claims/payments in Allegra.", Boundary.External);

    public static SoftwareSystem AllegraSystem =>
        new("AllegraSystem", "Allegra Debtors System",
            "Debtors/Claims system integrated with PD environment.", Boundary.External);
    public static SoftwareSystem PowerBiTracker =>
        new("PowerBiTracker", "Remittance Advice Tracker (Power BI)",
            "Power BI report showing the status of remittance advice processing.", Boundary.External);

    public static SoftwareSystem PowerBiReport =>
        new("PowerBiReport", "Remittance Advice Tracker (Power BI)",
            "Power BI report showing the status of remittance advice processing.", Boundary.External);

}