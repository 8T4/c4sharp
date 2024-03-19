// See https://aka.ms/new-console-template for more information

using C4Sharp.Diagrams;
using C4Sharp.Diagrams.Plantuml;
using C4Sharp.Diagrams.Themes;
using ModelDiagrams.Diagrams;

var diagrams = new DiagramBuilder[]
{
    new ContextDiagramSample(),
    new ComponentDiagramSample(),
    new ContainerDiagramSample(),
    new EnterpriseDiagramSample(),
    new SequenceDiagramSample(),
    new DeploymentDiagramSample()
};

var path = Path.Combine("..", "..", "..", "..", "..", "docs", "images");
        
new PlantumlContext()
    .UseDiagramImageBuilder()
    .UseDiagramSvgImageBuilder()
    .Export(path, diagrams, new ParadisoTheme());