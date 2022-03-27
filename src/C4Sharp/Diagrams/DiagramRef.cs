namespace C4Sharp.Diagrams;

internal static class DiagramRef
{
    public static string LinkTo<T>() where T : DiagramBuildRunner => GetLink(typeof(T));
    public static string CreateRef(this DiagramBuildRunner diagram) => GetLink(diagram.GetType());
    public static string CreateRef(this Diagram diagram) => GetLink(diagram.GetType());
    private static string GetLink(Type type) => $"dgref.{type.FullName?.ToLower() ?? type.Name.ToLower()}";
}

public static class DiagramHRef
{
    public static string LinkTo<T>() where T : DiagramBuildRunner => $"{DiagramRef.LinkTo<T>()}.html";
}