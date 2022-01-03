using C4Bank.Deposit.Shared;
using C4Bank.Deposit.UseCases.DepositoProcessing.Adapters;
using C4Bank.Deposit.UseCases.SynchronizeNewAccount.Interfaces;
using C4Sharp.Diagrams;
using C4Sharp.Models;
using C4Sharp.Models.Containers;
using C4Sharp.Models.Plantuml;
using C4Sharp.Models.Plantuml.Constants;
using C4Sharp.Models.Relationships;

namespace C4Bank.Deposit.Architecure;

public class ComponentDiagram : DiagramBuildRunner
{
    protected override string Title => "C4Bank Componetns of Deposit Area";
    protected override DiagramType DiagramType => DiagramType.Component;

    protected override IEnumerable<Structure> Structures() => new Structure[]
    {
        new Person("Customer", "Customer"),
        new EventStreaming<IEvent>("kafka", "Partition")[1],
        new EventStreaming<IEvent>("kafka", "Partition")[2],
        new EventStreaming<IEvent>("kafka", "DLQ")[3],
        new ContainerBoundary<DepositoProcessingWorker>
        {
            Components = new Component[]
            {
                new Component<IController>("Aspnet"),
                new Component<IWorker>("class/C#"),
                new Component<ICommand>("record/C#"),
                new Component<IValidation>("decorator/C#"),
                new Component<IHandler>("class/C#"),
                new Component<IProducer>("class/C#"),
                new Component<IRepository>("class/C#"),
            }
        },
        new Database("Database", "Database", "SQL Server", "Data Base")
    };

    protected override IEnumerable<Relationship> Relationships() => new[]
    {
        It("Customer") > It<IController>() | "request",
        It<IController>() > It<IEvent>(1) | "Produces",
        
        //Worker
        It<IWorker>() > It<IEvent>(1) | "Consumes",
        It<IWorker>() > It<ICommand>() | "Map to",
        It<IWorker>() > It<IValidation>() | "Call",

        //Validation
        It<IValidation>() > It<IHandler>() |"Call",
        It<IValidation>() > It<IProducer>() | "Produces",
        
        //Handler
        It<IHandler>() > It<ICommand>() | "Execute",
        It<IHandler>() >= It<IRepository>() | "Write/Read",
        It<IHandler>() > It<IProducer>() | "Produces",
        
        //Producer
        It<IProducer>() > It<IEvent>(2) | "Produces",
        It<IProducer>() > It<IEvent>(3) | "Produces",
        It<IRepository>() >= It("Database") | "Write/Read",
    };

    protected override IElementStyle? SetStyle()
    {
        return new ElementStyle()
            .UpdateElementStyle(ElementName.Person, "#000000", "#000000")
            .UpdateElementStyle(ElementName.Component, "#ffffff", "#000000", "#000000", false, Shape.RoundedBoxShape)
            .UpdateElementStyle(ElementName.Container, "#f4f4f4", "#000000", "#000000", false, Shape.RoundedBoxShape);
    }
}