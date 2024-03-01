using System.Reflection.Emit;
using C4Sharp.Elements;
using C4Sharp.Elements.Containers;

namespace C4Sharp.Sample.Structures;

public static class Containers
{
    private static ServerSideWebApp? _webApp;

    public static ServerSideWebApp WebApp => _webApp ??= new ServerSideWebApp(
        Alias: "Corporate.Finance.Limits.Service.ServiceBus",
        Label: "WebApp",
        Description: "Delivers the static content and the Internet banking SPA",
        Technology: "C#, WebApi"
    );

    private static ClientSideWebApp? _spa;

    public static ClientSideWebApp Spa => _spa ??= new ClientSideWebApp(
        Alias: "Spa",
        Label: "SPA",
        Technology: "JavaScript, Angular",
        Description: "Provides all the Internet banking functionality to customers via their web browser"
    );

    private static Mobile? _mobileApp;

    public static Mobile MobileApp => _mobileApp ??= new Mobile(
        Alias: "MobileApp",
        Label: "MobileApp",
        Description:
        "Provides a limited subset of the Internet banking functionality to customers via their mobile device",
        Technology: "C#, Xamarin"
    );

    private static Database? _sqlDatabase;

    public static Database SqlDatabase => _sqlDatabase ??= new Database(
        Alias: "Database",
        Label: "SqlDatabase",
        Description: "Stores user registration information, hashed auth credentials, access logs, etc.",
        Technology: "SQL Database"
    );

    private static Queue? _rabbitMq = null;

    public static Queue RabbitMq => _rabbitMq ?? new Queue(
        Alias: "Queue",
        Label: "RabbitMQ",
        Description: "Stores user registration information, hashed auth credentials, access logs, etc.",
        Technology: "RabbitMQ"
    );

    private static Database? _oracleDatabase;

    public static Database OracleDatabase => _oracleDatabase ??= new Database(
        Alias: "Database",
        Label: "OracleDatabase",
        Description: "Stores user registration information, hashed auth credentials, access logs, etc.",
        Technology: "Oracle Database"
    );

    private static Api? _backendApi;

    public static Api BackendApi => _backendApi ??= new Api(
        Alias: "BackendApi",
        Label: "BackendApi",
        Description: "Provides Internet banking functionality via API.",
        Technology: "Dotnet, Docker Container"
    );
}