using C4Sharp.Diagrams;
using C4Sharp.Models.Plantuml.Extensions;

namespace C4Sharp.Models.Plantuml.IO;

public static class PlantumlStream
{
    private static readonly object Lock = new();

    public static (string fileName, Stream fileContent) GetStream(this PlantumlSession session, Diagram diagram)
    {
        lock (Lock)
        {
            var puml = diagram.ToPumlString(session.StandardLibraryBaseUrl);
            return session.GetStream(puml);
        }
    }
}
