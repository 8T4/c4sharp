using C4Sharp.Models;

namespace C4Sharp.Sample.Structures
{
    public static class Components
    {
        private static Component _sign;
        public static Component Sign => _sign ??= new Component(
            alias: "sign",
            label: "Sign In Controller",
            description: "Allows users to sign in to the internet banking system",
            technology: "MVC Rest Controller"
        );

        private static Component _accounts;
        public static Component Accounts => _accounts ??= new Component(
            alias: "accounts",
            label: "Accounts Summary Controller",
            description: "Provides customers with a summary of their bank accounts",
            technology: "MVC Rest Controller"
        );

        private static Component _security;
        public static Component Security => _security ??= new Component(
            alias: "security",
            label: "Security Component",
            description: "Provides functionality related to singing in, changing passwords, etc.",
            technology: "Spring Bean"
        );

        private static Component _mainframeFacade;
        public static Component MainframeFacade => _mainframeFacade ??= new Component(
            alias: "mbsfacade",
            label: "Mainframe Banking System Facade",
            description: "A facade onto the mainframe banking system.",
            technology: "C#, Class Library"
        );
    }
}