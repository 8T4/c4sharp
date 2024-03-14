namespace C4Sharp.Diagrams.Interfaces;

[Obsolete("This interface is obsolete, use IDiagramBuilder instead")]
public interface IDiagramBuildRunner
{
    Diagram Build();
}

public interface IDiagramBuilder
{
    Diagram Build(IDiagramTheme? theme);
}