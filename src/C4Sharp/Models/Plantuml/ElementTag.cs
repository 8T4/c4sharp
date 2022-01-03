using C4Sharp.Diagrams;
using C4Sharp.Models.Plantuml.Constants;

namespace C4Sharp.Models.Plantuml;

public class ElementTag : IElementTag
{
    public IDictionary<string, string> Items { get; } = new Dictionary<string, string>();

    /// <summary>
    /// Introduces a new element tag. The styles of the tagged elements are updated and the tag
    /// is displayed in the calculated legend.
    /// </summary>
    /// <param name="tagStereo"></param>
    /// <param name="bgColor"></param>
    /// <param name="fontColor"></param>
    /// <param name="borderColor"></param>
    /// <param name="shadowing"></param>
    /// <param name="shape"></param>
    /// <exception cref="ArgumentNullException">
    /// If tagStereo is Null or Empty Value
    /// </exception>
    /// <returns></returns>
    public ElementTag AddElementTag(string tagStereo, string bgColor, string fontColor = "#ffffff", string borderColor = "#00000000", bool shadowing = false, Shape? shape = null)
    {
        if (string.IsNullOrEmpty(tagStereo))
        {
            throw new ArgumentNullException(nameof(tagStereo), $"{nameof(tagStereo)} is required");
        }

        var value = shape is null
            ? $"AddElementTag(\"{tagStereo}\", $bgColor={bgColor}, $fontColor={fontColor}, $borderColor={borderColor}, $shadowing=\"{shadowing}\")"
            : $"AddElementTag(\"{tagStereo}\", $bgColor={bgColor}, $fontColor={fontColor}, $borderColor={borderColor}, $shadowing=\"{shadowing}\", $shape={shape.Value})";

        Items[tagStereo] = value;
        return this;
    }
}
