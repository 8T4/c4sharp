using C4Sharp.Models;

namespace C4Sharp.IntegratedTests.Stubs.Models;

public static class People
{
    private static Person _customer;

    public static Person Customer => _customer ??= new Person("customer", "Personal Banking Customer")
    {
        Description = "A customer of the bank, with personal bank accounts."
    };
}
