using C4Sharp.Models.Relationships;

namespace C4Sharp.Models.Plantuml.Extensions;

/// <summary>
/// Parser Relationship to PlantUML
/// </summary>
internal static class PlantumlRelationship
{
    /// <summary>
    /// Create PUML content from Relationship
    /// </summary>
    /// <param name="relationship"></param>
    /// <returns></returns>        
    public static string ToPumlString(this Relationship relationship)
    {
        var direction = relationship.Direction switch
        {
            Direction.Back => "Rel_Back",
            Direction.Forward => "Rel",
            Direction.Bidirectional => "BiRel",
            _ => "Rel"
        };

        direction += relationship.Position switch
        {
            Position.Down => "_D",
            Position.Up => "_U",
            Position.Left => "_L",
            Position.Right => "_R",
            Position.Neighbor => "_Neighbor",
            Position.None => "",
            _ => ""
        };

        var tags = relationship.Tags.Any()
            ? $", $tags={string.Join("+", relationship.Tags)}"
            : string.Empty;

        return string.IsNullOrEmpty(relationship.Protocol)
            ? $"{direction}({relationship.From}, {relationship.To}, \"{relationship.Label}\"{tags})"
            : $"{direction}({relationship.From}, {relationship.To}, \"{relationship.Label}\", \"{relationship.Protocol}\"{tags})";
    }
}
