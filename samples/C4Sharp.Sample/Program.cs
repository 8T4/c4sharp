using C4Sharp.Models.Plantuml;
using C4Sharp.Models.Plantuml.Constants;
using C4Sharp.Models.Plantuml.IO;
using C4Sharp.Sample.Diagrams;

var Style = new ElementStyle()
    .UpdateElementStyle(ElementName.ExternalPerson, "#7f3b08", "#7f3b08")
    .UpdateElementStyle(ElementName.Person, "#55ACEE", "#55ACEE")
    .UpdateElementStyle(ElementName.ExternalSystem, "#3F6684", shape: Shape.RoundedBoxShape);

var Reltags = new RelationshipTag()
    .AddRelTag("error", "red", "red", LineStyle.DashedLine);

var Tags = new ElementTag()
    .AddElementTag("services", "#3F6684", shape: Shape.EightSidedShape);

var diagrams = new[]
{
    ContextDiagramBuilder.Build().SetStyle(Style).SetRelTags(Reltags),
    ContainerDiagramBuilder.Build().SetTags(Tags),
    ComponentDiagramBuilder.Build(),
    DeploymentDiagramBuilder.Build(),
    EnterpriseDiagramBuilder.Build().SetStyle(Style),
};

new PlantumlSession()
    .UseDiagramImageBuilder()
    .UseDiagramSvgImageBuilder()
    .UseStandardLibraryBaseUrl()
    .Export(diagrams);