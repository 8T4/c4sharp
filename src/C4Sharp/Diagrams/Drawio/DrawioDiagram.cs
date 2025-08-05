using System.Collections.Generic;
using System.Xml.Serialization;

namespace C4Sharp.Diagrams.Drawio
{
    [XmlRoot("mxGraphModel", Namespace = "")]
    public class MxGraphModel
    {
        [XmlAttribute("dx")]
        public string Dx { get; set; } = "1434";

        [XmlAttribute("dy")]
        public string Dy { get; set; } = "790";

        [XmlAttribute("grid")]
        public int Grid { get; set; } = 1;

        [XmlAttribute("gridSize")]
        public int GridSize { get; set; } = 10;

        [XmlAttribute("guides")]
        public int Guides { get; set; } = 1;

        [XmlAttribute("tooltips")]
        public int Tooltips { get; set; } = 1;

        [XmlAttribute("connect")]
        public int Connect { get; set; } = 1;

        [XmlAttribute("arrows")]
        public int Arrows { get; set; } = 1;

        [XmlAttribute("fold")]
        public int Fold { get; set; } = 1;

        [XmlAttribute("page")]
        public int Page { get; set; } = 1;

        [XmlAttribute("pageScale")]
        public int PageScale { get; set; } = 1;

        [XmlAttribute("pageWidth")]
        public int PageWidth { get; set; } = 850;

        [XmlAttribute("pageHeight")]
        public int PageHeight { get; set; } = 1100;

        [XmlElement("root")]
        public Root Root { get; set; }
    }

    public class Root
    {
        [XmlElement("mxCell")]
        public List<MxCell> MxCells { get; set; } = new List<MxCell>();
    }

    public class MxCell
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("value")]
        public string Value { get; set; }

        [XmlAttribute("style")]
        public string Style { get; set; }

        [XmlAttribute("parent")]
        public string Parent { get; set; }

        [XmlAttribute("vertex")]
        public int Vertex { get; set; }

        [XmlAttribute("edge")]
        public int Edge { get; set; }

        [XmlAttribute("source")]
        public string Source { get; set; }

        [XmlAttribute("target")]
        public string Target { get; set; }

        [XmlElement("mxGeometry")]
        public MxGeometry Geometry { get; set; }
    }

    public class MxGeometry
    {
        [XmlAttribute("x")]
        public double X { get; set; }

        [XmlAttribute("y")]
        public double Y { get; set; }

        [XmlAttribute("width")]
        public double Width { get; set; }

        [XmlAttribute("height")]
        public double Height { get; set; }

        [XmlAttribute("as")]
        public string As { get; set; }
    }
}
