using C4Sharp.Elements;
using C4Sharp.Elements.Containers;
using C4Sharp.Elements.Relationships;

namespace ModelDiagrams.Structures;

public static class Containers
{
    public static ServerSideWebApp WebApp => new (
        Alias: "Corporate.Finance.Limits.Service.ServiceBus",
        Label: "WebApp",
        Description: "Delivers the static content and the Internet banking SPA",
        Technology: "C#, WebApi"
    );

    public static ClientSideWebApp SpaApp => new (
        Alias: "Spa",
        Label: "SPA",
        Technology: "JavaScript, Angular",
        Description: "Provides all the Internet banking functionality to customers via their web browser"
    );

    public static Mobile MobileApp => new (
        Alias: "MobileApp",
        Label: "MobileApp",
        Description:
        "Provides a limited subset of the Internet banking functionality to customers via their mobile device",
        Technology: "C#, Xamarin"
    );
    public static Mobile MailSystem => new(
    Alias: "MobileApp",
    Label: "MobileApp",
    Description:
    "Provides a limited subset of the Internet banking functionality to customers via their mobile device",
    Technology: "C#, Xamarin"
);
    
    public static Mobile Mainframe => new(
        Alias: "MobileApp",
        Label: "MobileApp",
        Description:
        "Provides a limited subset of the Internet banking functionality to customers via their mobile device",
        Technology: "C#, Xamarin"
    );

    public static Database SqlDatabase => new (
        Alias: "Database",
        Label: "SqlDatabase",
        Description: "Stores user registration information, hashed auth credentials, access logs, etc.",
        Technology: "SQL Database"
    );

    public static Queue RabbitMq => new (
        Alias: "Queue",
        Label: "RabbitMQ",
        Description: "Stores user registration information, hashed auth credentials, access logs, etc.",
        Technology: "RabbitMQ"
    );

    public static Database OracleDatabase =>  new (
        Alias: "Database",
        Label: "OracleDatabase",
        Description: "Stores user registration information, hashed auth credentials, access logs, etc.",
        Technology: "Oracle Database"
    );

    public static Api BackendApi => new(
        Alias: "BackendApi",
        Label: "BackendApi",
        Description: "Provides Internet banking functionality via API.",
        Technology: "Dotnet, Docker Container"
    );


    public static SoftwareSystem PharmacyDirectSystem = new SoftwareSystem(
        "Pharmacy Direct System",
        "Handles bank/ERA ingestion, claims, accounting, and reporting.");

    // Containers inside Pharmacy Direct
    public static Api PowerAutomateWorker = new Api(
        "Power Automate + PD Worker Service",
        "Fetch/import CSV/XLS or ERA files from SharePoint/SFTP into SQL",
        "Microsoft Power Automate + Custom Worker");

    public static Database SqlDataWarehouse = new Database(
        "SQL Data Warehouse",
        "Central PD data store for bank, ERA, claims, etc.",
        "Microsoft SQL Server");

    public static Api RemittanceApiGateway = new Api(
        "Remittance API Gateway",
        "Receives consolidated or age-analysis reports from MediMatch, publishes to RabbitMQ",
        "ASP.NET Core Web API");

    public static Queue RabbitMqQueue = new Queue(
        "RabbitMQ Queue",
        "Message broker to feed remittance data to the Processor service",
        "RabbitMQ");

    public static Api RemittanceProcessor = new Api(
        "Remittance Processor Service",
        "Applies reconciliation logic, updates claims/payments in Allegra",
        "C# Service");

    public static SoftwareSystem AllegraSystem = new SoftwareSystem(
        "Allegra Debtors System",
        "Debtors/Claims system integrated with PD environment",
        "Allegra platform");

    public static SoftwareSystem PowerBiTracker = new SoftwareSystem(
        "Remittance Advice Tracker (Power BI)",
        "BI dashboards for real-time reconciliation insights",
        "Microsoft Power BI");

    public static SoftwareSystem PastelAccounting = new SoftwareSystem(
        "Pastel Accounting System",
        "Used by Finance team for GL exports, final ledger",
        "Sage Pastel");
}