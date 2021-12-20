using System.Collections.Generic;

namespace C4Sharp.Diagrams
{
    public interface IDiagramStyle
    {
        public IDictionary<string, string> Items { get; }
    }
}