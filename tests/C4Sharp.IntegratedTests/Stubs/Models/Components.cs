using C4Sharp.Models;

namespace C4Sharp.IntegratedTests.Stubs.Models;

public static class Components
{
    private static Component _sign;

    public static Component Sign => _sign ??= new Component("sign", "Sign In Controller")
    {
        Description = "Allows users to sign in to the internet banking system",
        Technology = "MVC Rest Controller",
    };

    private static Component _accounts;

    public static Component Accounts => _accounts ??= new Component("accounts", "Accounts Summary Controller")
    {
        Description = "Provides customers with a summary of their bank accounts",
        Technology = "MVC Rest Controller"
    };

    private static Component _security;

    public static Component Security => _security ??= new Component("security", "Security Component")
    {
        Description = "Provides functionality related to singing in, changing passwords, etc.",
        Technology = "Spring Bean"
    };

    private static Component _mainframeFacade;

    public static Component MainframeFacade => _mainframeFacade ??=
        new Component("mbsfacade", "Mainframe Banking System Facade")
        {
            Description = "A facade onto the mainframe banking system.",
            Technology = "C#, Class Library"
        };
}
