using C4Sharp.Tools;
using C4Sharp.Tools.Root;

await RootCommandBuilder
    .CreateDefaultBuilder(args)
    .Configure<Startup>()
    .Run();
    

