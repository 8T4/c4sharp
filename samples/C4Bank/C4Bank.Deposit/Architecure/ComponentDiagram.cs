using C4Bank.Deposit.Shared;
using C4Bank.Deposit.UseCases.DepositoProcessing.Adapters;
using C4Bank.Deposit.UseCases.DepositoProcessing.Interfaces;
using C4Bank.Deposit.UseCases.DepositoProcessing.UseCase.Messages.Events;
using C4Bank.Deposit.UseCases.SynchronizeNewAccount.Adapters;
using C4Bank.Deposit.UseCases.SynchronizeNewAccount.Interfaces;
using C4Bank.Deposit.UseCases.SynchronizeNewAccount.UseCase.Messages.Events;
using C4Sharp.Diagrams;
using C4Sharp.Models;
using C4Sharp.Models.Containers;
using C4Sharp.Models.Relationships;

namespace C4Bank.Deposit.Architecure;

public class ComponentDiagram : DiagramBuildRunner
{
    public override string Title => "Componetns of C4Bank Deposit Area";
    public override DiagramType DiagramType => DiagramType.Component;


    protected override IEnumerable<Structure> Structures() => new Structure[]
    {
        new Person("Customer", "Customer", "Bank Customer"),
        new SoftwareSystem("OTBank.Finance", "Finance", "OTBank Finance System", Boundary.External),
        new Api<DepositReceived>("Aspnet/C#", "ACL"),

        new ContainerBoundary<DepositoProcessingConsumer>
        {
            Components = new Component[]
            {
                new Component<IConsumer>(),
                new Component<IHandler>()
            }
        },
    };

    protected override IEnumerable<Relationship> Relationships() => new[]
    {
        It<IConsumer>() > It<IHandler>()
    };
}