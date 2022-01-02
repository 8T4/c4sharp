using C4Sharp.Models.Plantuml;
using C4Sharp.Models.Plantuml.Constants;
using C4Sharp.Models.Plantuml.IO;
using C4Sharp.Sample.Diagrams;

namespace C4Sharp.Sample;

internal static class Program
{
    private static readonly ElementStyle Style = new ElementStyle()
        .UpdateElementStyle(ElementName.ExternalPerson, "#7f3b08", "#7f3b08")
        .UpdateElementStyle(ElementName.Person, "#55ACEE", "#55ACEE")
        .UpdateElementStyle(ElementName.ExternalSystem, "#3F6684", shape: Shape.RoundedBoxShape);

    private static readonly RelationshipTag Reltags = new RelationshipTag()
        .AddRelTag("error", "red", "red", LineStyle.DashedLine);

    private static readonly ElementTag Tags = new ElementTag()
        .AddElementTag("services", "#3F6684", shape: Shape.EightSidedShape);

    private static void Main()
    {
        var diagrams = new[]
        {
            new ContextDiagramBuildRunner().Build().SetStyle(Style).SetRelTags(Reltags),
            new ContainerDiagramBuildRunner().Build().SetTags(Tags),
            new ComponentDiagramBuildRunner().Build(),
            new DeploymentDiagramBuildRunner().Build(),
            new EnterpriseDiagramBuildRunner().Build().SetStyle(Style),
        };
        new PlantumlSession()
            .UseDiagramImageBuilder()
            .UseDiagramSvgImageBuilder()
            .UseStandardLibraryBaseUrl()
            .Export(diagrams);
    }
}