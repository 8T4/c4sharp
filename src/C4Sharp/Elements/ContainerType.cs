﻿using System.ComponentModel;

namespace C4Sharp.Elements;

/// <summary>
/// Container Type
/// </summary>
public enum ContainerType
{
    [Description("Mobile app")]
    Mobile,

    [Description("Server-side web application")]
    WebApplication,

    [Description("Server-side console application")]
    ServerConsole,

    [Description("Client-side desktop application")]
    ClientDesktop,

    [Description("Serverless function")]
    ServerlessFunction,

    [Description("Blob or content store")]
    Blob,

    [Description("File sytem")]
    FileSystem,

    [Description("Shell script")]
    ShellScript,

    [Description("SPA")]
    Spa,

    [Description("API")]
    Api,
    
    [Description("Microservice")]
    Microservice,

    [Description("Queue")]
    Queue,
    
    [Description("Topic")]
    Topic,    

    [Description("Database")]
    Database,

    [Description("")]
    None
}
