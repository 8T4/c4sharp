using C4Sharp.Diagrams.Interfaces;
using C4Sharp.Diagrams.Plantuml.Constants;
using C4Sharp.Diagrams.Plantuml.Style;

namespace C4Sharp.Diagrams.Themes;

public class DefaultTheme: IDiagramTheme
{
    private const string ComponentBackground = "#85bbf0";
    private const string ComponentBorder = "#78a8d9";
    private const string ComponentText = "#000000";
    
    private const string ContainerBackground = "#438dd4";
    private const string ContainerBorder = "#3e82c5";
    private const string ContainerText = "#FFFFFF";
    
    private const string PersonBackground = "#0d437b";
    private const string PersonBorder = "#0d437b";
    private const string PersonText = "#FFFFFF";
    
    private const string ExternalBackground = "#999999";
    private const string ExternalBorder = "#8a8a8a";
    private const string ExternalText = "#FFFFFF";

    private const string SystemBackground = "#1a67be";
    private const string SystemBorder = "#175eaa";
    private const string SystemText = "#FFFFFF";

    public IElementStyle? Style => new ElementStyle()
        .UpdateElementStyle(ElementName.System, SystemBackground, SystemText, SystemBorder, false, Shape.RoundedBoxShape, BorderStyle.SolidLine, 2)
        .UpdateElementStyle(ElementName.ExternalSystem, ExternalBackground, ExternalText, ExternalBorder, false, Shape.RoundedBoxShape, BorderStyle.SolidLine, 1)
        .UpdateElementStyle(ElementName.Person, PersonBackground, PersonText, PersonBorder, false, Shape.RoundedBoxShape, BorderStyle.SolidLine, 2)
        .UpdateElementStyle(ElementName.Component, ComponentBackground, ComponentText, ComponentBorder, false, Shape.RoundedBoxShape, BorderStyle.SolidLine, 2)
        .UpdateElementStyle(ElementName.ExternalComponent, ExternalBackground, ExternalText, ExternalBorder, false, Shape.RoundedBoxShape, BorderStyle.SolidLine, 1)
        .UpdateElementStyle(ElementName.Container, ContainerBackground, ContainerText, ContainerBorder, false, Shape.RoundedBoxShape, BorderStyle.SolidLine, 2)
        .UpdateElementStyle(ElementName.ExternalContainer, ExternalBackground, ExternalText, ExternalBorder, false, Shape.RoundedBoxShape, BorderStyle.SolidLine, 1);

    public IBoundaryStyle? BoundaryStyle => new BoundaryStyle()
        .UpdateBoundaryStyle(ElementName.System, "#FFFFFF", "#000000", "#000000", false, Shape.RoundedBoxShape)
        .UpdateBoundaryStyle(ElementName.Container, "#FFFFFF", "#000000", "#000000", false, Shape.RoundedBoxShape)
        .UpdateBoundaryStyle(ElementName.Enterprise, "#FFFFFF", "#000000", "#000000", false, Shape.RoundedBoxShape);

    public IElementTag? Tags { get; } = null;
    public IRelationshipTag? RelTags { get; } = null;
}