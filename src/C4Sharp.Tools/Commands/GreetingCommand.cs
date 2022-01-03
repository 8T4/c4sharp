using System.CommandLine;
using System.CommandLine.Invocation;

namespace C4Sharp.Tools.Commands;

public class GreetingCommand: Command
{
    public GreetingCommand() : base("hi", "Say Hi.")
    {
        Handler = CommandHandler.Create(SayHi);
    }

    private const string Banner =
        "░█████╗░░░██╗██╗░██████╗██╗░░██╗░█████╗░██████╗░██████╗░  ░█████╗░██╗░░░░░██╗\n" +
        "██╔══██╗░██╔╝██║██╔════╝██║░░██║██╔══██╗██╔══██╗██╔══██╗  ██╔══██╗██║░░░░░██║\n" +
        "██║░░╚═╝██╔╝░██║╚█████╗░███████║███████║██████╔╝██████╔╝  ██║░░╚═╝██║░░░░░██║\n" +
        "██║░░██╗███████║░╚═══██╗██╔══██║██╔══██║██╔══██╗██╔═══╝░  ██║░░██╗██║░░░░░██║\n" +
        "╚█████╔╝╚════██║██████╔╝██║░░██║██║░░██║██║░░██║██║░░░░░  ╚█████╔╝███████╗██║\n" +
        "░╚════╝░░░░░░╚═╝╚═════╝░╚═╝░░╚═╝╚═╝░░╚═╝╚═╝░░╚═╝╚═╝░░░░░  ░╚════╝░╚══════╝╚═╝\n";
    
    private static void SayHi()
    {
        Console.WriteLine(Banner);
    }
}