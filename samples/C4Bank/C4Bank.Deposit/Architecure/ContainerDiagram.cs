using C4Bank.Deposit.UseCases.DepositoProcessing.Adapters;
using C4Bank.Deposit.UseCases.DepositoProcessing.Interfaces;
using C4Bank.Deposit.UseCases.DepositoProcessing.UseCase.Messages.Events;
using C4Bank.Deposit.UseCases.SynchronizeNewAccount.Adapters;
using C4Bank.Deposit.UseCases.SynchronizeNewAccount.Interfaces;
using C4Bank.Deposit.UseCases.SynchronizeNewAccount.UseCase.Messages.Commands;
using C4Bank.Deposit.UseCases.SynchronizeNewAccount.UseCase.Messages.Events;
using C4Sharp.Diagrams;
using C4Sharp.Models;
using C4Sharp.Models.Containers;
using C4Sharp.Models.Plantuml;
using C4Sharp.Models.Plantuml.Constants;
using C4Sharp.Models.Relationships;

namespace C4Bank.Deposit.Architecure;

public class ContainerDiagram : DiagramBuildRunner
{
    protected override string Title => "C4Bank Context of Deposit Area";
    protected override DiagramType DiagramType => DiagramType.Container;
    

    protected override IEnumerable<Structure> Structures() => new Structure[]
    {
        new Person("Customer", "Customer", "Bank Customer"),
        new SoftwareSystem("OTBank.Finance", "Finance", "OTBank Finance System", Boundary.External),
        new SoftwareSystem("C4Bank.Account", "Account", "C4Bank Account System"),
        new Api<DepositReceived>("Aspnet/C#", "ACL"),
        new EventStreaming<RegisteredAccount>("kafka", "Partition 01"),
        
        SoftwareSystemBoundary.New("Deposit",
            new Api<DepositoProcessingWorker>("C#"),
            new Database<IDepositRepository>("SQL Server", "Deposit Data Base"),
            new ServerConsole<SynchronizeNewAccountConsumer>("C#", "Kafka Consumer"),
            new Database<IAccountRepository>("SQL Server", "Account Data Base")
        ),
    };

    protected override IEnumerable<Relationship> Relationships() => new[]
    {
        It("Customer") > It("OTBank.Finance") | "send deposit",
        It("OTBank.Finance") > It<DepositReceived>() | ("POST", "HTTP"),
        It<DepositoProcessingWorker>() < It<DepositReceived>() | ("POST", "HTTP"),
        It<DepositoProcessingWorker>() > It<IDepositRepository>(),
        
        It("Customer") > It("C4Bank.Account") | "register",
        It("C4Bank.Account") > It<RegisteredAccount>() | "produces",
        It<SynchronizeNewAccountConsumer>() > It<RegisteredAccount>() | "consumes",
        It<SynchronizeNewAccountConsumer>() > It<IAccountRepository>(),
        It<DepositoProcessingWorker>() > It<IAccountRepository>(),
    };
    
    protected override IElementStyle? SetStyle()
    {
        return new ElementStyle()
            .UpdateElementStyle(ElementName.Person, "#000000", "#000000")
            .UpdateElementStyle(ElementName.Container, "#ffffff", "#000000", "#000000", false, Shape.RoundedBoxShape)
            .UpdateElementStyle(ElementName.System, "#f4f4f4", "#000000", "#000000", false, Shape.RoundedBoxShape)
            .UpdateElementStyle(ElementName.ExternalSystem, "#ABB2B9", "#000000", "#000000", false, Shape.RoundedBoxShape);
    }    
}