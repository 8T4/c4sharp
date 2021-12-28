using C4Sharp.Diagrams;
using C4Sharp.Models.Plantuml.IO;
using C4Sharp.Sample.Orders;

var diagrams = new Diagram[]
{
    PackageByLayerDiagram.Diagram,
};

using var session = new PlantumlSession();
session.Export(diagrams);