<p align="center">
<img src="https://raw.githubusercontent.com/8T4/c4sharp/main/docs/images/8t4-c4-brand-2.png" alt="logo" width='400'>  
</p>

C4Sharp (`C4S`) is a .net library for building [C4 Model diagrams](https://c4model.com/). It's works like a superset of [C4-PlantUML](https://github.com/plantuml-stdlib/C4-PlantUML) through which developers can create, share, and consume [C4 Model diagrams](https://c4model.com/) as code (C#) such as Context, Container, Component and Deployment diagrams.

[![.NET](https://github.com/8T4/c4sharp/actions/workflows/dotnet.yml/badge.svg)](https://github.com/8T4/c4sharp/actions/workflows/dotnet.yml)
[![CodeQL](https://github.com/8T4/c4sharp/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/8T4/c4sharp/actions/workflows/codeql-analysis.yml)

## Getting Started

### Instalation
This package is available through Nuget Packages: https://www.nuget.org/packages/C4Sharp

| Package |  Version | Downloads | Maintainability |
| ------- | ----- | ----- |----- |
| `C4Sharp` | [![NuGet](https://img.shields.io/nuget/v/C4Sharp.svg)](https://www.nuget.org/packages/C4Sharp) | [![Nuget](https://img.shields.io/nuget/dt/C4Sharp.svg)](https://www.nuget.org/packages/C4Sharp) | [![Codacy Badge](https://app.codacy.com/project/badge/Grade/51ea16a0d91548cb9e84bd6ab3e8cb9e)](https://www.codacy.com/gh/8T4/c4sharp/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=8T4/c4sharp&amp;utm_campaign=Badge_Grade) |

**Nuget**
```shell
Install-Package C4Sharp
```

**.NET CLI**
```shell
dotnet add package C4Sharp
```

### Dependencies

You need these things to run C4Sharp:
 - [.NET Standard 2.1](https://docs.microsoft.com/pt-br/dotnet/standard/net-standard)
 - [Java](https://www.java.com/en/download/)
 - [Graphviz](https://plantuml.com/graphviz-dot) 

### Coding

C4S diagrams have a basic structure containing a Title, a set of C4 structures and their Relationships.
As shown in following code:

```c#
var diagram = new <Diagram Type>
{
    Title = "...",
    Structures = new Structure[] { },
    Relationships = new Relationship[]{ }
};
```

With C4S you can create a _C4 Context Diagram_, as shown in following [code](https://github.com/8T4/c4sharp/tree/main/src/C4Sharp.Sample):

```c#

var diagram = new ContextDiagram
{
    Title = "System Context diagram for Internet Banking System",
    Structures = new Structure[]
    {
        Customer,
        BankingSystem,
        Mainframe,
        MailSystem
    },
    Relationships = new []
    {
        (Customer > BankingSystem),
        (Customer < MailSystem)["Sends e-mails to"],
        (BankingSystem > MailSystem)["Sends e-mails", "SMTP"][Neighbor],
        (BankingSystem > Mainframe),
    }
};

PlantumlFile.Export(diagram);
```


The result will be:

<p align="center">
  <img src="https://raw.githubusercontent.com/8T4/c4sharp/main/docs/images/context-example.png" alt="logo" width='450' >
</p>

## Learn

To learn more about `C4S` access our [wiki](https://github.com/8T4/c4sharp/wiki).

## Thanks
### C4 community
- [Simon Brown](https://twitter.com/simonbrown)
- [PlantUML Team](https://plantuml.com/)
- [C4-PlantUML Team](https://github.com/plantuml-stdlib/C4-PlantUML)

### Colleagues 
- [Abraão Honório](https://www.linkedin.com/in/abraaohonorio)
- [Daniel Martins](https://www.linkedin.com/in/daniel-de-souza-martins)
- [Rafel Santos](https://www.linkedin.com/in/rafael-santos-0b51465)

## Guide to contributing to a GitHub project
This is a guide to contributing to this open source project that uses GitHub. It’s mostly based on how many open sorce projects operate. That’s all there is to it. The fundamentals are:

 - Fork the project & clone locally.
 - Create an upstream remote and sync your local copy before you branch.
 - Branch for each separate piece of work.
 - Do the work, write good commit messages, and read the CONTRIBUTING file if there is one.
 - Push to your origin repository.
 - Create a new PR in GitHub.
 - Respond to any code review feedback.

If you want to contribute to an open source project, the best one to pick is one that you are using yourself. The maintainers will appreciate it!
