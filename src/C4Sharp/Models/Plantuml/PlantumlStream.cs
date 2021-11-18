using System.Collections.Generic;
using System.IO;
using C4Sharp.Diagrams;

namespace C4Sharp.Models.Plantuml
{
    public static class PlantumlStream
    {
        private static readonly object Lock = new object();

        public static (string, Stream) GetStream(this PlantumlSession session, Diagram diagram)
        {
            lock (Lock)
            {
                var puml = diagram.ToPumlString(session.StandardLibraryBaseUrl);
                return session.GetStream(puml);
            }
        }
    }
}