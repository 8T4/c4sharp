using System.Reflection.Metadata.Ecma335;
using C4Sharp.Diagrams.Core;
using C4Sharp.Diagrams.Supplementary;
using C4Sharp.Extensions;
using C4Sharp.Models;
using C4Sharp.Models.Relationships;

namespace C4Sharp.Diagrams;

public abstract class DiagramBuildRunner: IDiagramBuildRunner
{
    private readonly StructureCollection _structures;
    
    protected bool LayoutWithLegend { get; set; }
    protected bool ShowLegend { get; set; }
    protected bool LayoutAsSketch { get; set; }
    protected string? Title { get; set; }
    protected DiagramLayout FlowVisualization { get; set; }    
    public abstract DiagramType DiagramType { get; }

    
    protected DiagramBuildRunner()
    {
        _structures = new StructureCollection();
    }

    public Structure? It<T>() => _structures.Items[StructureIdentity.New<T>().Value];
    public Structure? It<T>(int instance) => _structures.Items[StructureIdentity.New<T>(instance.ToString()).Value];
    public Structure? It<T>(string instance) => _structures.Items[StructureIdentity.New<T>(instance).Value];
    public Structure? It(string key) => _structures.Items[key];
    public Structure? It(string key, int instance) => _structures.Items[new StructureIdentity(key, instance.ToString()).Value];
    public Structure? It(string key, string instance) => _structures.Items[new StructureIdentity(key, instance).Value];


    protected abstract IEnumerable<Structure> Structures();
    protected abstract IEnumerable<Relationship> Relationships();
    public virtual IElementStyle? SetStyle() => null;
    public virtual IElementTag? SetTags() => null;
    public virtual IRelationshipTag? SetRelTags() => null;    
    
    
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
            Title = Title ?? $"{GetType().Name.SplitCapitalizedWords()}",
            ShowLegend = ShowLegend,
            LayoutWithLegend = LayoutWithLegend,
            LayoutAsSketch = LayoutAsSketch,
            FlowVisualization = FlowVisualization
        };


        result.SetStyle(SetStyle());
        result.SetTags(SetTags());
        result.SetRelTags(SetRelTags());

        return result;
    }
}