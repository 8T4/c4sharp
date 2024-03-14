using C4Sharp.Elements.Containers;

namespace ModelDiagrams.Structures;

public static class Containers
{
    public static ServerSideWebApp WebApp => new (
        Alias: "Corporate.Finance.Limits.Service.ServiceBus",
        Label: "WebApp",
        Description: "Delivers the static content and the Internet banking SPA",
        Technology: "C#, WebApi"
    );

    public static ClientSideWebApp Spa => new (
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
}