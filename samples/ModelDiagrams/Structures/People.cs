using C4Sharp.Elements;
using C4Sharp.Elements.Relationships;

namespace ModelDiagrams.Structures;

public static class People
{
    public static Person Customer => new ("customer", "Personal Banking Customer", 
        "A customer of the bank, with personal bank accounts.", Boundary.External);

    public static Person InternalCustomer => new Person("internalcustomer", "Personal Banking Customer", 
        "An customer of the bank, with personal bank accounts.");

    public static Person Manager => new ("manager", "Manager Banking Customer", 
        "A manager of the bank, with personal bank accounts.");
    public static Person FinanceTeam = new Person(
           "Finance Team",
           "Human actors verifying and approving exceptions");
    public static Person MedicalAidSchemes = new Person(
              "Medical Aid Schemes",
              "External providers of ERA files");


}
