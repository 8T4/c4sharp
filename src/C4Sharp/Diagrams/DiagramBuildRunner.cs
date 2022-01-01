using System.Reflection.Metadata.Ecma335;
using C4Sharp.Diagrams.Core;
using C4Sharp.Diagrams.Supplementary;
using C4Sharp.Extensions;
using C4Sharp.Models;
using C4Sharp.Models.Relationships;

namespace C4Sharp.Diagrams;

public abstract class DiagramBuildRunner : IDiagramBuildRunner
{
    private readonly StructureCollection _structures;

    protected bool LayoutWithLegend { get; set; } = false;
    protected bool ShowLegend { get; set; } = false;
    protected bool LayoutAsSketch { get; set; } = false;
    protected DiagramLayout FlowVisualization { get; set; } = DiagramLayout.TopDown;


    public abstract string Title { get; }
    public abstract DiagramType DiagramType { get; }


    protected DiagramBuildRunner()
    {
        _structures = new StructureCollection();
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


    protected abstract IEnumerable<Structure> Structures();
    protected abstract IEnumerable<Relationship> Relationships();
    protected virtual IElementStyle? SetStyle() => null;
    protected virtual IElementTag? SetTags() => null;
    protected virtual IRelationshipTag? SetRelTags() => null;


    public Diagram Build()
    {
        _structures.AddRange(Structures());

        Diagram? diagram = DiagramType.Value switch
        {
            DiagramConstants.Component => Activator.CreateInstance<ComponentDiagram>(),
            DiagramConstants.Container => Activator.CreateInstance<ContainerDiagram>(),
            DiagramConstants.Context => Activator.CreateInstance<ContextDiagram>(),
            DiagramConstants.Deployment => Activator.CreateInstance<DeploymentDiagram>(),
            _ => throw new ArgumentNullException(nameof(DiagramType), $"{nameof(DiagramType)} is required")
        };

        var result = diagram with
        {
            Structures = Structures().ToArray(),
            Relationships = Relationships().ToArray(),
            Title = Title,
            ShowLegend = ShowLegend,
            LayoutWithLegend = LayoutWithLegend,
            LayoutAsSketch = LayoutAsSketch,
            FlowVisualization = FlowVisualization
        };

        return result.SetStyle(SetStyle())
            .SetTags(SetTags())
            .SetRelTags(SetRelTags());
    }
}