using C4Sharp.Models;

namespace C4Sharp.IntegratedTests.Stubs.Models;

public static class Containers
{
    private static Container _webApp;

    public static Container WebApp => _webApp ??= new Container(
        "WebApp", "WebApp")
    {
        ContainerType = ContainerType.WebApplication,
        Description = "Delivers the static content and the Internet banking SPA",
        Technology = "C#, WebApi"
    };

    private static Container _spa;

    public static Container Spa => _spa ??= new Container(
        "Spa", "SPA")
    {
        ContainerType = ContainerType.Spa,
        Description = "Provides all the Internet banking functionality to cutomers via their web browser",
        Technology = "JavaScript, Angular"
    };

    private static Container _mobileApp;

    public static Container MobileApp => _mobileApp ??= new Container(
        "MobileApp", "MobileApp")
    {
        ContainerType = ContainerType.Mobile,
        Description =
            "Provides a limited subset of the Internet banking functionality to customers via their mobile device",
        Technology = "C#, Xamarin"
    };

    private static Container _sqlDatabase;

    public static Container SqlDatabase => _sqlDatabase ??= new Container(
        "Database", "SqlDatabase")
    {
        ContainerType = ContainerType.Database,
        Description = "Stores user registration information, hashed auth credentials, access logs, etc.",
        Technology = "SQL Database"
    };

    private static Container _oracleDatabase;

    public static Container OracleDatabase => _oracleDatabase ??= new Container(
        "Database", "OracleDatabase")
    {
        ContainerType = ContainerType.Database,
        Description = "Stores user registration information, hashed auth credentials, access logs, etc.",
        Technology = "Oracle Database"
    };

    private static Container _backendApi;

    public static Container BackendApi => _backendApi ??= new Container(
        "BackendApi", "BackendApi")
    {
        ContainerType = ContainerType.Api,
        Description = "Provides Internet banking functionality via API.",
        Technology = "Dotnet, Docker Container"
    };
}
