
<div style="text-align: center">
<picture>
  <source
    src="https://raw.githubusercontent.com/8T4/c4sharp/main/docs/images/8t4-c4-brand-pb.png"
    media="(prefers-color-scheme: dark)">
  <img src="https://raw.githubusercontent.com/8T4/c4sharp/main/docs/images/8t4-c4-brand.png" alt= "logo" height="200">
</picture>
</div>

# C4Sharp

C4Sharp (C4S) is a simple .NET superset of C4-PlantUML to generate C4 diagrams as code (C#). 
It's used for building Context, Container, Component and Deployment diagrams.

### Diagrams

To build C4 Diagrams in C4S it's simple. Just use this following structure for all
diagrams:

```c#
var diagram = new <Diagram Type>
{
    Title = "...",
    Structures = new Structure[] { },
    Relationships = new Relationship[]{ }
};
```
For example to create a Context Diagram you could type this code:

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
    Relationships = new Relationship[]
    {
        new Relationship(Customer, BankingSystem, "Uses"),
        new RelateBack(Customer, MailSystem, "Sends e-mails to"),
        new RelateNeighbor(BankingSystem, MailSystem, "Sends e-mails", "SMTP"),
        new Relationship(BankingSystem, Mainframe, "Uses"),
    }
};

PlantumlFile.Save(diagram);
PlantumlFile.ExportToPng(diagram);
```
This code results two files:

- ./c4/System_Context_diagram_for_Internet_Banking_System_C4_Context.puml
- ./c4/System_Context_diagram_for_Internet_Banking_System_C4_Context.png

The result will be:

<div style="text-align: center">

![context-example](docs/images/context-example.png)

</div>
