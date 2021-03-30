
<img src="https://raw.githubusercontent.com/8T4/c4sharp/main/docs/images/8t4-c4-brand-2.png" alt="logo" width='400' >

C4Sharp (C4S) is a .NET superset of [C4-PlantUML](https://github.com/plantuml-stdlib/C4-PlantUML) through which developers can create, share, and consume [C4 Model diagrams](https://c4model.com/) as code (C#) such as Context, Container, Component and Deployment diagrams.

## Getting Started

### Instalation
This package is available through Nuget Packages: https://www.nuget.org/packages/C4Sharp

| Package |  Version | Downloads |
| ------- | ----- | ----- |
| `C4Sharp` | [![NuGet](https://img.shields.io/nuget/v/C4Sharp.svg)](https://www.nuget.org/packages/C4Sharp) | [![Nuget](https://img.shields.io/nuget/dt/C4Sharp.svg)](https://www.nuget.org/packages/C4Sharp) |


**Nuget**
```
Install-Package C4Sharp
```

**.NET CLI**
```
dotnet add package C4Sharp
```

### Dependencies

You need these things to run C4Sharp:
- [.NET Standard 2.1](https://docs.microsoft.com/pt-br/dotnet/standard/net-standard)
- [Java](https://www.java.com/en/download/)
- [Graphviz](https://plantuml.com/graphviz-dot) 


### Coding

Use the following structure for all C4S diagrams:

```c#
var diagram = new <Diagram Type>
{
    Title = "...",
    Structures = new Structure[] { },
    Relationships = new Relationship[]{ }
};
```

For example, with C4S you can create a _C4 Context Diagram_ using this code:

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

PlantumlFile.Save(diagram);
PlantumlFile.Export(diagram);
```
The result will be:

<p align="center">
  <img src="https://raw.githubusercontent.com/8T4/c4sharp/main/docs/images/context-example.png" alt="logo" width='450' >
</p>

## Learn

See more samples [here](https://github.com/8T4/c4sharp/tree/main/tests/C4Sharp.Tests/C4Model/Samples)


## Background

#### PlantUML

> [PlantUML](http://en.plantuml.com/) is an open source project that allows you to create UML diagrams.
> Diagrams are defined using a simple and intuitive language.
> Images can be generated in PNG, in SVG or in LaTeX format.
> PlantUML was created to allow the drawing of UML diagrams, using a simple and human readable text description.
> Because it does not prevent you from drawing inconsistent diagrams, it is a drawing tool and not a modeling tool.
> It is the most used text-based diagram drawing tool with [extensive support into wikis and forums, text editors and IDEs, use by different programming languages >and documentation generators](http://en.plantuml.com/running).

#### C4 model

> The [C4 model](https://c4model.com/) for software architecture is an "abstraction-first" approach to diagramming, based upon abstractions that reflect how 
> software architects and developers think about and build software.
> The small set of abstractions and diagram types makes the C4 model easy to learn and use.
> C4 stands for context, containers, components, and code — a set of hierarchical diagrams that you can use to describe your software architecture at different 
> zoom levels, each useful for different audiences.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details
