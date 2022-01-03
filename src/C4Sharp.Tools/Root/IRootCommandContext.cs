namespace C4Sharp.Tools.Root;

public interface IRootCommandContext
{
    IRootCommandContext Add<TCommand>() where TCommand: Command;
    
    Task<int> InvokeAsync(string[] args);
}