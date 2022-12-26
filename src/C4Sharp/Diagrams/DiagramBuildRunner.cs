using C4Sharp.Diagrams.Interfaces;
using C4Sharp.Elements;
using C4Sharp.Elements.Relationships;

namespace C4Sharp.Diagrams;

public abstract class DiagramBuildRunner : IDiagramBuildRunner
{
    private readonly StructureCollection _structures;

    protected virtual bool LayoutWithLegend { get; }
    protected virtual bool ShowLegend { get; }
    protected virtual bool LayoutAsSketch { get; }
    protected virtual DiagramLayout FlowVisualization { get; }
    protected abstract string Title { get; }
    protected virtual string Description { get; }
    protected abstract DiagramType DiagramType { get; }
    protected abstract IEnumerable<Structure> Structures { get; }
    protected abstract IEnumerable<Relationship> Relationships { get;  }
    
    protected DiagramBuildRunner()
    {
        _structures = new StructureCollection();
        LayoutWithLegend = false;
        Description = string.Empty;
        ShowLegend = false;
        LayoutAsSketch = false;
        FlowVisualization = DiagramLayout.TopDown;
    }

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

    public Diagram Build()
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
            Tags = SetTags(),
            RelTags = SetRelTags(),
            Style = SetStyle()
        };
    }
}