using C4Sharp.Elements;

namespace ModelDiagrams.Structures;

public static class Components
{
    public static Component Sign =>  
        new ("sign", "Sign In Controller", "MVC Controller", "Allows users to sign in to the internet banking system");

    public static Component Accounts => 
        new ("accounts", "Accounts Summary Controller", "MVC Controller", "Provides customers with a summary of their bank accounts");

    public static Component Security =>
        new ("security", "Security Component", "Spring Bean", "Provides functionality related to singing in, changing passwords, etc.");

    public static Component MainframeFacade =>
        new ("mbsfacade", "Mainframe Banking System Facade", "Spring Bean", "A facade onto the mainframe banking system.");
}
