using System.Collections.Generic;
using C4Sharp.Models;

namespace C4Sharp.Tests.C4Model
{
    public static class Persons
    {
        public static Person Customer => new(
            alias: "customer",
            label: "Personal Banking Customer",
            description: "A customer of the bank, with personal bank accounts."
        );
    }

    public static class Systems
    {
        public static SoftwareSystem BankingSystem => new(
            alias: "BankingSystem",
            label: "Internet Banking System",
            description: "Allows customers to view information about their bank accounts, and make payments."
        );

        public static SoftwareSystem Mainframe => new(
            alias: "Mainframe",
            label: "Mainframe Banking System",
            description: "Stores all of the core banking information about customers, accounts, transactions, etc.",
            softwareSystemType: SoftwareSystemType.External
        );

        public static SoftwareSystem MailSystem => new(
            alias: "MailSystem",
            label: "E-mail system",
            description: "The internal Microsoft Exchange e-mail system.",
            softwareSystemType: SoftwareSystemType.External
        );

        public static SoftwareSystem Zabbix => new(
            alias: "Zabbix",
            label: "Zabbix",
            description: "enterprise-level platform to monitor large-scale IT environments",
            softwareSystemType: SoftwareSystemType.External
        );
    }

    public static class Containers
    {
        public static Container WebApp => new(
            alias: "WebApp",
            type: ContainerType.WebApplication,
            description: "Delivers the static content and the Internet banking SPA",
            technology: "C#, WebApi"
        );

        public static Container Spa => new(
            alias: "Spa",
            type: ContainerType.Spa,
            description: "Provides all the Internet banking functionality to cutomers via their web browser",
            technology: "JavaScript, Angular"
        );

        public static Container MobileApp => new(
            alias: "MobileApp",
            type: ContainerType.Mobile,
            description:
            "Provides a limited subset of the Internet banking functionality to customers via their mobile device",
            technology: "C#, Xamarin"
        );

        public static Container SqlDatabase => new(
            alias: "Database",
            type: ContainerType.Database,
            description: "Stores user registration information, hashed auth credentials, access logs, etc.",
            technology: "SQL Database"
        );
        
        public static Container OracleDatabase => new(
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

        public static Container BackendApi => new(
            alias: "BackendApi",
            type: ContainerType.Api,
            description: "Provides Internet banking functionality via API.",
            technology: "Dotnet, Docker Container"
        );
    }

    public static class Components
    {
        public static Component Sign => new(
            alias: "sign",
            label: "Sign In Controller",
            description: "Allows users to sign in to the internet banking system",
            technology: "MVC Rest Controller"
        );

        public static Component Accounts => new(
            alias: "accounts",
            label: "Accounts Summary Controller",
            description: "Provides customers with a summary of their bank accounts",
            technology: "MVC Rest Controller"
        );

        public static Component Security => new(
            alias: "security",
            label: "Security Component",
            description: "Provides functionality related to singing in, changing passwords, etc.",
            technology: "Spring Bean"
        );

        public static Component MainframeFacade => new(
            alias: "mbsfacade",
            label: "Mainframe Banking System Facade",
            description: "A facade onto the mainframe banking system.",
            technology: "C#, Class Library"
        );
    }

    public static class Nodes
    {
        public static DeploymentNode ApacheTomCat(string alias, Container container) =>
            new(alias, "Apache Tomcat", "Apache Tomcat 8.x")
            {
                Properties = new Dictionary<string, string>
                {
                    ["Java Version"] = "8",
                    ["Xmx"] = "512M",
                    ["Xms"] = "1024M",
                },
                Container = container
            };

        public static DeploymentNode OracleNode(string alias, Container container) => 
            new(alias, "Oracle - Primary", "Oracle 12c")
        {
            Container = container
        };

        public static DeploymentNode Ubuntu(string alias, string label, DeploymentNode node) =>
            new(alias, label, "Ubuntu 16.04 LTS")
            {
                Nodes = new[] {node}
            };
        
        public static DeploymentNode Ios(string alias, Container container) =>
            new(alias, "Customer's mobile device", "Apple IOS")
            {
                Container = container
            };        
        
        public static DeploymentNode Android(string alias, Container container) =>
            new(alias, "Customer's mobile device", "Android")
            {
                Container = container
            };

        public static DeploymentNode PersonalComputer(string alias, DeploymentNode node) =>
            new(alias, "Customer's computer", "Mircosoft Windows of Apple macOS")
            {
                Nodes =  new []{node}
            };      
        
        public static DeploymentNode Browser(string alias, Container container) =>
            new(alias, "Web Browser", "Google Chrome, Mozilla Firefox, Apple Safari or Microsoft Edge")
            {
                Container = container
            };         
    }
}