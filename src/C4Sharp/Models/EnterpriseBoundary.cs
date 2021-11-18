using System;
using System.Collections.Generic;

namespace C4Sharp.Models
{
    public record EnterpriseBoundary(string Alias, string Label) : Structure(Alias, Label)
    {
        public IEnumerable<Structure> Structures { get; init; } = Array.Empty<Structure>();        
    }
}