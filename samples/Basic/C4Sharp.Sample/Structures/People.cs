using C4Sharp.Models;
using C4Sharp.Models.Relationships;

namespace C4Sharp.Sample.Structures;

public static class People
{
    private static Person _customer;

    public static Person Customer => _customer ??= new Person("customer", "Personal Banking Customer")
    {
        Description = "A customer of the bank, with personal bank accounts.",
        Boundary = Boundary.External
    };

    private static Person _internalCustomer;
    public static Person InternalCustomer => _internalCustomer ??= new Person("internalcustomer", "Personal Banking Customer")
    {
        Description = "An internal customer of the bank, with personal bank accounts."
    };

    private static Person _manager;
    public static Person Manager => _manager ??= new Person("manager", "Manager Banking Customer")
    {
        Description = "A manager of the bank, with personal bank accounts."
    };
}
