global using ColoredConsole;
global using Microsoft.Build.Locator;
global using Microsoft.CodeAnalysis;
global using Microsoft.CodeAnalysis.CSharp;
global using Microsoft.CodeAnalysis.CSharp.Syntax;
global using Microsoft.CodeAnalysis.MSBuild;
global using System.CommandLine;
global using System.CommandLine.Invocation;

using C4Sharp.Tools.Commands;
using C4Sharp.Tools.Root;

namespace C4Sharp.Tools;

internal class Startup
{
    public void Configuration(IRootCommandContext context)
    {
        context
            .Add<GreetingCommand>()
            .Add<BuildCommand>();
    }
}