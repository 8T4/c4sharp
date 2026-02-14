using System.Collections.Generic;
using System.IO;

namespace C4Sharp.Diagrams.Drawio
{
    public class DrawioContext
    {
        private readonly List<DiagramBuilder> _diagrams = new();
        private bool _generateSvg = false;
        private bool _generatePng = false;

        public DrawioContext AddDiagrams(params DiagramBuilder[] diagrams)
        {
            _diagrams.AddRange(diagrams);
            return this;
        }

        public DrawioContext UseSvgExport()
        {
            _generateSvg = true;
            return this;
        }

        public DrawioContext UsePngExport()
        {
            _generatePng = true;
            return this;
        }

        public void Export(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach (var diagram in _diagrams)
            {
                var builtDiagram = diagram.Build();
                
                // Always export XML
                DrawioDiagramExporter.Export(path, builtDiagram);
                
                // Export SVG if requested
                if (_generateSvg)
                {
                    DrawioSvgExporter.ExportSvg(path, builtDiagram);
                }
                
                // Export PNG if requested
                if (_generatePng)
                {
                    DrawioPngExporter.ExportPng(path, builtDiagram);
                }
            }
        }
    }
}
