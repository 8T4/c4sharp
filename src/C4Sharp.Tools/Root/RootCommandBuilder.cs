namespace C4Sharp.Tools.Root;

public class RootCommandBuilder
{
    private readonly IRootCommandContext _context;
    private readonly string[] _args;
    
    private RootCommandBuilder(string[] args)
    {
        _context = new RootCommandContext();
        _args = args;
    }

    public static RootCommandBuilder CreateDefaultBuilder(string[] args)
    {
        return new RootCommandBuilder(args);
    }

    public RootCommandBuilder Configure<TStartup>() where TStartup: class
    {
        var startup = Activator.CreateInstance<TStartup>() as dynamic;
        startup?.Configuration(_context);
        return this;
    }
    
    public RootCommandBuilder Configure(Action<IRootCommandContext> action)
    {
        action.Invoke(_context);
        return this;
    }    

    public async Task Run() => await _context.InvokeAsync(_args);

}