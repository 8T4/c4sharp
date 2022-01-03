using C4Sharp.Diagrams;
using C4Sharp.Models;
using C4Sharp.Models.Relationships;
using static C4Sharp.Models.Relationships.Position;

namespace C4Bank.Deposit.Architecure;

public class ContextDiagram : DiagramBuildRunner
{
    public override string Title => "Contexts of C4Bank Deposit System";
    public override DiagramType DiagramType => DiagramType.Context;
    

    protected override IEnumerable<Structure> Structures() => new Structure[]
    {
        new Person("Customer", "Customer", "Bank Customer"),
        new SoftwareSystem("OTBank.Finance", "Finance", "OTBank Finance System", Boundary.External),
        
        new EnterpriseBoundary("C4Bank", "C4Bank Domain",
            new SoftwareSystem("C4Bank.Account", "Account", "C4Bank Account System", Boundary.Internal),
            new SoftwareSystem("C4Bank.Deposit", "Deposit", "C4Bank Deposit System", Boundary.Internal)),
        
        new SoftwareSystem("eMailer.System", "Deposit", "Mailer Deposit System", Boundary.External),
    };

    protected override IEnumerable<Relationship> Relationships() => new[]
    {
        (It("Customer") > It("OTBank.Finance")) ["Request deposit"],
        (It("OTBank.Finance") > It("C4Bank.Deposit")) ["Send deposit"],
        
        (It("Customer") > It("C4Bank.Account")) ["Request registration"],
        (It("C4Bank.Account") > It("C4Bank.Deposit")) ["Send registration"],
        
        (It("C4Bank.Deposit") > It("eMailer.System")) ["Notify"][Neighbor],
        (It("eMailer.System") > It("Customer")) ["Notify customer"]
    };
}