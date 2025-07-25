using C4Sharp.Diagrams.Interfaces;
using C4Sharp.Elements;
using C4Sharp.Elements.Relationships;

namespace C4Sharp.Diagrams;

/// <summary>
/// Base class to build diagrams
/// </summary>
public abstract partial class DiagramBuilder : IDiagramBuilder
{
    private readonly StructureCollection _structures = new();

    protected virtual bool LayoutWithLegend { get; } = false;
    protected virtual bool ShowLegend { get; } = false;
    protected virtual bool LayoutAsSketch { get; } = false;
    protected virtual DiagramLayout FlowVisualization { get; } = DiagramLayout.TopDown;
    protected abstract string Title { get; }
    protected virtual string Description { get; } = string.Empty;
    protected abstract DiagramType DiagramType { get; }
    protected abstract IEnumerable<Structure> Structures { get; }
    protected abstract IEnumerable<Relationship> Relationships { get; }
    
    public Diagram Build(IDiagramTheme? theme = null)
    {
        _structures.AddRange(Structures);
        
        return new Diagram(DiagramType) with
        {
            Structures = Structures,
            Relationships = Relationships,
            Title = Title,
            ShowLegend = ShowLegend,
            Description = Description,
            LayoutWithLegend = LayoutWithLegend,
            LayoutAsSketch = LayoutAsSketch,
            FlowVisualization = FlowVisualization,
            Tags = SetTags() ?? theme?.Tags,
            RelTags = SetRelTags() ?? theme?.RelTags,
            Style = SetStyle() ?? theme?.Style,
            BoundaryStyle = SetBoundaryStyle() ?? theme?.BoundaryStyle 
        };
    }    
}

/// <summary>
/// Custom methods to build diagrams
/// </summary>
public abstract partial class DiagramBuilder
{
    public Structure this[string key] => It(key);
    public Structure this[string key, int instance] => It(key, instance);
    public Structure this[string key, string instance] => It(key, instance);
    
    public Structure It<T>() => It(StructureIdentity.New<T>().Value);
    public Structure It<T>(int instance) => It(StructureIdentity.New<T>(instance.ToString()).Value);
    public Structure It<T>(string instance) => It(StructureIdentity.New<T>(instance).Value);

    public Structure It(string key)
        => _structures.Items[key] 
           ?? throw new KeyNotFoundException($"Structure {key} not found");

    public Structure It(string key, int instance)
        => _structures.Items[new StructureIdentity(key, instance.ToString()).Value] 
           ?? throw new KeyNotFoundException($"Structure {key} not found");

    public Structure It(string key, string instance)
        => _structures.Items[new StructureIdentity(key, instance).Value]
           ?? throw new KeyNotFoundException($"Structure {key} not found");

    protected virtual IElementStyle? SetStyle() => null;
    protected virtual IElementTag? SetTags() => null;
    protected virtual IRelationshipTag? SetRelTags() => null;
    protected virtual IBoundaryStyle? SetBoundaryStyle() => null;
}