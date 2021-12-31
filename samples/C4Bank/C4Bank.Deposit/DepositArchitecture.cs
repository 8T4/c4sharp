using C4Bank.Deposit.DepositoProcessing.Adapters;
using C4Bank.Deposit.DepositoProcessing.Interfaces;
using C4Bank.Deposit.DepositoProcessing.UseCase.Messages.Events;
using C4Bank.Deposit.SynchronizeNewAccount.Interfaces;
using C4Bank.Deposit.SynchronizeNewAccount.UseCase.Messages.Commands;
using C4Sharp.Diagrams;
using C4Sharp.Diagrams.Core;
using C4Sharp.Models.Relationships;
using C4Sharp.Models;
using C4Sharp.Models.Containers;

namespace C4Bank.Deposit;

public class DepositArchitecture : DiagramBuildRunner
{
    public DepositArchitecture()
    {
        Title = "C4Bank Deposit Area";
    }

    public override DiagramType DiagramType => DiagramType.Container;

    protected override IEnumerable<Structure> Structures() => new Structure[]
    {
        new Person("Customer", "Customer", "Bank Customer"),
        new SoftwareSystem("C4Bank.Finance", "Finance", "C4Bank Finance System"),
        new EventStreaming<DepositReceived>("kafka", "Partition 01")[1],
        new EventStreaming<DepositReceived>("kafka", "Partition 02")[2],
        
        SoftwareSystemBoundary.New("Deposit",
            new ServerConsole<DepositoProcessingConsumer>("C#", "Kafka Consumer"),
            new Database<IDepositRepository>("SQL Server", "Deposit Data Base"),
            new ServerConsole<SynchronizedAccountCommand>("C#", "Kafka Consumer"),
            new ServerConsole<IAccountRepository>("SQL Server", "Account Data Base")
        )
    };

    protected override IEnumerable<Relationship> Relationships() => new[]
    {
        (It("Customer") > It("C4Bank.Finance")) ["send deposit"],
        (It("C4Bank.Finance") > It<DepositReceived>(1)) ["produces"],
        (It("C4Bank.Finance") > It<DepositReceived>(2)) ["produces"],
        
        (It<DepositoProcessingConsumer>() > It<DepositReceived>(1)) ["consumes"],
        (It<DepositoProcessingConsumer>() > It<DepositReceived>(2)) ["consumes"],
        It<DepositoProcessingConsumer>() > It<IDepositRepository>()
    };
}