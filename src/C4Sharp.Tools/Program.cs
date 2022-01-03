using C4Sharp.Tools.Commands;
using C4Sharp.Tools.Root;

var root = RootCommandBuilder
    .CreateDefaultBuilder(args)
    .Configure(context =>
    {
        context
            .Add<GreetingCommand>()
            .Add<BuildCommand>();
    });

await root.Run();