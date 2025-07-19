namespace C4Sharp.Diagrams.Interfaces;

public interface IDiagramBuilder
{
    Diagram Build(IDiagramTheme? theme);
}