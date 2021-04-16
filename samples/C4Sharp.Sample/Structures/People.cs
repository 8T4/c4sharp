using C4Sharp.Models;

namespace C4Sharp.Sample.Structures
{
    public static class People
    {
        private static Person _customer;
        public static Person Customer => _customer??= new Person(
            alias: "customer",
            label: "Personal Banking Customer",
            description: "A customer of the bank, with personal bank accounts."
        );
    }
}