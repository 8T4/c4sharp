using C4Sharp.Diagrams.Interfaces;
using C4Sharp.Diagrams.Plantuml.Constants;
using C4Sharp.Diagrams.Plantuml.Style;

namespace C4Sharp.Diagrams.Themes;

public record ParadisoTheme: IDiagramTheme
{
    private const string ComponentBackground = "#FAE5D3";
    private const string ComponentBorder = "#CA6F1E";
    private const string ComponentText = "#CA6F1E";
    
    private const string ContainerBackground = "#EAF2F8";
    private const string ContainerBorder = "#2E86C1";
    private const string ContainerText = "#2E86C1";
    
    private const string PersonBackground = "#797D7F";
    private const string PersonBorder = "#797D7F";
    private const string PersonText = "#797D7F";
    
    private const string ExternalBackground = "#F8F9F9";
    private const string ExternalBorder = "#797D7F";
    private const string ExternalText = "#797D7F";

    private const string SystemBackground = "#D4E6F1";
    private const string SystemBorder = "#1A5276";
    private const string SystemText = "#1A5276";

    public IElementStyle? Style => new ElementStyle()
        .UpdateElementStyle(ElementName.System, SystemBackground, SystemText, SystemBorder, false, Shape.RoundedBoxShape, BorderStyle.SolidLine, 2)
        .UpdateElementStyle(ElementName.ExternalSystem, ExternalBackground, ExternalText, ExternalBorder, false, Shape.RoundedBoxShape, BorderStyle.SolidLine, 1)
        .UpdateElementStyle(ElementName.Person, PersonBackground, PersonText, PersonBorder, false, Shape.RoundedBoxShape, BorderStyle.SolidLine, 2)
        .UpdateElementStyle(ElementName.Component, ComponentBackground, ComponentText, ComponentBorder, false, Shape.RoundedBoxShape, BorderStyle.SolidLine, 2)
        .UpdateElementStyle(ElementName.ExternalComponent, ExternalBackground, ExternalText, ExternalBorder, false, Shape.RoundedBoxShape, BorderStyle.SolidLine, 1)
        .UpdateElementStyle(ElementName.Container, ContainerBackground, ContainerText, ContainerBorder, false, Shape.RoundedBoxShape, BorderStyle.SolidLine, 2)
        .UpdateElementStyle(ElementName.ExternalContainer, ExternalBackground, ExternalText, ExternalBorder, false, Shape.RoundedBoxShape, BorderStyle.SolidLine, 1);

    public IBoundaryStyle? BoundaryStyle => new BoundaryStyle()
        .UpdateBoundaryStyle(ElementName.System, ExternalBackground, ExternalText, ExternalBorder, false, Shape.RoundedBoxShape)
        .UpdateBoundaryStyle(ElementName.Container, ExternalBackground, ExternalText, ExternalBorder, false, Shape.RoundedBoxShape)
        .UpdateBoundaryStyle(ElementName.Enterprise, ExternalBackground, ExternalText, ExternalBorder, false, Shape.RoundedBoxShape);

    public IElementTag? Tags { get; } = null;
    public IRelationshipTag? RelTags { get; } = null;
}