using C4Sharp.Diagrams;
using C4Sharp.Models.Plantuml.Constants;

namespace C4Sharp.Models.Plantuml;

public class RelationshipTag : IRelationshipTag
{
    public IDictionary<string, string> Items { get; } = new Dictionary<string, string>();

    /// <summary>
    /// Introduces a new relation tag. The styles of the tagged relations are updated and the tag is displayed
    /// in the calculated legend.
    /// Perform this PUML ddRelTag(tagStereo, ?textColor, ?lineColor, ?lineStyle)
    /// </summary>
    /// <param name="tagStereo"></param>
    /// <param name="textColor"></param>
    /// <param name="lineColor"></param>
    /// <param name="lineStyle"></param>
    /// <returns></returns>
    public RelationshipTag AddRelTag(string tagStereo, string textColor, string lineColor = "#000000", LineStyle? lineStyle = null)
    {
        if (string.IsNullOrEmpty(tagStereo))
        {
            throw new ArgumentNullException(nameof(tagStereo), $"{nameof(tagStereo)} is required");
        }

        var value = lineStyle is null
            ? $"AddRelTag(\"{tagStereo}\", $textColor={textColor}, $lineColor={lineColor})"
            : $"AddRelTag(\"{tagStereo}\", $textColor={textColor}, $lineColor={lineColor}, $lineStyle={lineStyle.Value})";

        Items[tagStereo] = value;
        return this;
    }
}
