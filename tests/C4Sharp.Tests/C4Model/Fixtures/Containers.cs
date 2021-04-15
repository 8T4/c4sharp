using C4Sharp.Models;

namespace C4Sharp.Tests.C4Model.Fixtures
{
    public static class Containers
    {
        private static Container _webApp;
        public static Container WebApp => _webApp ??= new Container(
            alias: "WebApp",
            type: ContainerType.WebApplication,
            description: "Delivers the static content and the Internet banking SPA",
            technology: "C#, WebApi"
        );

        private static Container _spa;
        public static Container Spa => _spa ??= new Container(
            alias: "Spa",
            type: ContainerType.Spa,
            description: "Provides all the Internet banking functionality to cutomers via their web browser",
            technology: "JavaScript, Angular"
        );

        private static Container _mobileApp; 
        public static Container MobileApp => _mobileApp ??= new Container(
            alias: "MobileApp",
            type: ContainerType.Mobile,
            description:
            "Provides a limited subset of the Internet banking functionality to customers via their mobile device",
            technology: "C#, Xamarin"
        );

        private static Container _sqlDatabase;
        public static Container SqlDatabase => _sqlDatabase ??= new Container(
            alias: "Database",
            type: ContainerType.Database,
            description: "Stores user registration information, hashed auth credentials, access logs, etc.",
            technology: "SQL Database"
        );

        private static Container _oracleDatabase;
        public static Container OracleDatabase => _oracleDatabase ??= new Container(
            alias: "Database",
            type: ContainerType.Database,
            description: "Stores user registration information, hashed auth credentials, access logs, etc.",
            technology: "Oracle Database"
        );  
        
        public static Container OracleDatabaseInstance(string alias) => new(
            alias: alias,
            type: ContainerType.Database,
            description: "Stores user registration information, hashed auth credentials, access logs, etc.",
            technology: "Oracle Database"
        );         

        private static Container _backendApi;
        public static Container BackendApi => _backendApi ??= new Container(
            alias: "BackendApi",
            type: ContainerType.Api,
            description: "Provides Internet banking functionality via API.",
            technology: "Dotnet, Docker Container"
        );
    }
}