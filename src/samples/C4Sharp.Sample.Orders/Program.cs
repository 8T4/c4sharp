using System;
using C4Sharp.Diagrams;
using C4Sharp.Models.Plantuml;

namespace C4Sharp.Sample.Orders
{
    class Program
    {
        static void Main(string[] args)
        {
            var diagrams = new Diagram[]
            {
                PackageByLayerDiagram.Diagram,
            };

            using var session = new PlantumlSession();
            PlantumlFile.Export(diagrams, session);
        }
    }
}