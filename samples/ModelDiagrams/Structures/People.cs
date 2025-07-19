using C4Sharp.Elements;
using C4Sharp.Elements.Relationships;

namespace ModelDiagrams.Structures;

public static class People
{
    public static Person Customer => new(
        alias: "customer",
        label: "Personal Banking Customer",
        description: "A customer of the bank, with personal bank accounts.",
        boundary: Boundary.External
    );

    public static Person InternalCustomer => new(
        alias: "internalcustomer",
        label: "Personal Banking Customer",
        description: "An customer of the bank, with personal bank accounts."
    );

    public static Person Manager => new(
        alias: "manager",
        label: "Manager Banking Customer",
        description: "A manager of the bank, with personal bank accounts."
    );
}