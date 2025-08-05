using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using C4Sharp.Elements;
using C4Sharp.Elements.Boundaries;

namespace C4Sharp.Diagrams.Drawio
{
    public static class DrawioDiagramExporter
    {
        private static readonly Dictionary<string, string> TechnologyIcons = new()
        {
            { "react", "https://cdn.jsdelivr.net/gh/devicons/devicon/icons/react/react-original.svg" },
            { "angular", "https://cdn.jsdelivr.net/gh/devicons/devicon/icons/angularjs/angularjs-original.svg" },
            { "vue", "https://cdn.jsdelivr.net/gh/devicons/devicon/icons/vuejs/vuejs-original.svg" },
            { "azure", "https://cdn.jsdelivr.net/gh/devicons/devicon/icons/azure/azure-original.svg" },
            { "aws", "https://cdn.jsdelivr.net/gh/devicons/devicon/icons/amazonwebservices/amazonwebservices-original.svg" },
            { "gcp", "https://cdn.jsdelivr.net/gh/devicons/devicon/icons/googlecloud/googlecloud-original.svg" },
            { "kubernetes", "https://cdn.jsdelivr.net/gh/devicons/devicon/icons/kubernetes/kubernetes-plain.svg" },
            { "docker", "https://cdn.jsdelivr.net/gh/devicons/devicon/icons/docker/docker-original.svg" },
            { "python", "https://cdn.jsdelivr.net/gh/devicons/devicon/icons/python/python-original.svg" },
            { "tensorflow", "https://cdn.jsdelivr.net/gh/devicons/devicon/icons/tensorflow/tensorflow-original.svg" },
            { "kafka", "https://cdn.jsdelivr.net/gh/devicons/devicon/icons/apachekafka/apachekafka-original.svg" },
            { "prometheus", "https://cdn.jsdelivr.net/gh/devicons/devicon/icons/prometheus/prometheus-original.svg" },
            { "grafana", "https://cdn.jsdelivr.net/gh/devicons/devicon/icons/grafana/grafana-original.svg" }
        };

        public static void Export(string path, Diagram diagram)
        {
            var model = new MxGraphModel
            {
                Root = new Root()
            };

            // Add default cells for layer and background
            model.Root.MxCells.Add(new MxCell { Id = "0" });
            model.Root.MxCells.Add(new MxCell { Id = "1", Parent = "0" });

            var structures = diagram.Structures.ToList();
            var relationships = diagram.Relationships.ToList();
            
            // Enhanced layout calculation
            var positions = CalculateEnhancedLayout(structures.Count);
            var nodeWidth = 280;
            var nodeHeight = 200;

            // Add title if available
            if (!string.IsNullOrEmpty(diagram.Title))
            {
                model.Root.MxCells.Add(new MxCell
                {
                    Id = "title",
                    Value = diagram.Title,
                    Style = GetTitleStyle(),
                    Parent = "1",
                    Vertex = 1,
                    Geometry = new MxGeometry
                    {
                        X = 50,
                        Y = 20,
                        Width = 800,
                        Height = 40,
                        As = "geometry"
                    }
                });
            }

            // Create enhanced structure nodes
            for (var i = 0; i < structures.Count; i++)
            {
                var structure = structures[i];
                var position = positions[i];
                
                var cellValue = CreateEnhancedNodeContent(structure);
                
                model.Root.MxCells.Add(new MxCell
                {
                    Id = structure.Alias,
                    Value = cellValue,
                    Style = GetEnhancedStyle(structure),
                    Parent = "1",
                    Vertex = 1,
                    Geometry = new MxGeometry
                    {
                        X = position.x,
                        Y = position.y,
                        Width = nodeWidth,
                        Height = nodeHeight,
                        As = "geometry"
                    }
                });
            }

            // Create enhanced relationship edges
            foreach (var rel in relationships)
            {
                model.Root.MxCells.Add(new MxCell
                {
                    Id = $"{rel.From}-{rel.To}-{rel.Label}",
                    Value = rel.Label,
                    Style = GetEnhancedEdgeStyle(),
                    Parent = "1",
                    Edge = 1,
                    Source = rel.From,
                    Target = rel.To,
                    Geometry = new MxGeometry { As = "geometry" }
                });
            }

            var serializer = new XmlSerializer(typeof(MxGraphModel));
            var fileName = !string.IsNullOrEmpty(diagram.Title) 
                ? diagram.Title.Replace(" ", "-").Replace(":", "").Replace(",", "").ToLowerInvariant()
                : "diagram";
            var filePath = Path.Combine(path, $"{fileName}.drawio");

            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, model);
            }
        }

        private static string CreateEnhancedNodeContent(Structure structure)
        {
            var content = new StringBuilder();
            
            // Add technology icons if available
            var icons = GetTechnologyIcons(structure.Description);
            if (icons.Any())
            {
                content.Append("<div style='display: flex; align-items: center; margin-bottom: 8px;'>");
                foreach (var icon in icons.Take(3)) // Limit to 3 icons
                {
                    content.Append($"<img src='{icon}' width='20' height='20' style='margin-right: 4px;' />");
                }
                content.Append("</div>");
            }
            
            // Component title
            content.Append($"<div style='font-weight: bold; font-size: 14px; color: #0366d6; margin-bottom: 8px;'>{structure.Label}</div>");
            
            // Description
            if (!string.IsNullOrEmpty(structure.Description))
            {
                var shortDesc = structure.Description.Length > 80 
                    ? structure.Description.Substring(0, 77) + "..." 
                    : structure.Description;
                content.Append($"<div style='font-size: 11px; color: #586069; margin-bottom: 8px; line-height: 1.3;'>{shortDesc}</div>");
            }
            
            // Extract and display benefits and tools from description
            var (benefits, tools) = ExtractBenefitsAndTools(structure.Description);
            
            if (!string.IsNullOrEmpty(benefits))
            {
                content.Append($"<div style='font-size: 10px; color: #28a745; margin-bottom: 4px;'><strong>Benefits:</strong> {benefits}</div>");
            }
            
            if (!string.IsNullOrEmpty(tools))
            {
                content.Append($"<div style='font-size: 10px; color: #6f42c1;'><strong>Tools:</strong> {tools}</div>");
            }
            
            return content.ToString();
        }

        private static (string benefits, string tools) ExtractBenefitsAndTools(string description)
        {
            // Simple extraction logic - in a real implementation, you'd parse the JSON config
            var benefits = "";
            var tools = "";
            
            if (description.Contains("React") || description.Contains("Angular") || description.Contains("Vue"))
            {
                tools = "React/Angular/Vue, Azure/AWS/GCP";
            }
            else if (description.Contains("API") || description.Contains("integration"))
            {
                tools = "API Gateway, Apache NiFi, Kafka";
            }
            else if (description.Contains("Kubernetes") || description.Contains("microservices"))
            {
                tools = "Kubernetes, Istio, Camunda";
            }
            else if (description.Contains("AI") || description.Contains("ML"))
            {
                tools = "Azure ML, TensorFlow, MLflow";
            }
            
            return (benefits, tools);
        }

        private static List<string> GetTechnologyIcons(string description)
        {
            var icons = new List<string>();
            
            foreach (var tech in TechnologyIcons)
            {
                if (description.ToLowerInvariant().Contains(tech.Key))
                {
                    icons.Add(tech.Value);
                }
            }
            
            return icons;
        }

        private static List<(int x, int y)> CalculateEnhancedLayout(int count)
        {
            var positions = new List<(int x, int y)>();
            
            // Grid-based layout with better spacing
            var cols = Math.Min(3, count); // Max 3 columns
            var rows = (int)Math.Ceiling((double)count / cols);
            
            var startX = 100;
            var startY = 100;
            var spacingX = 320; // Wider spacing for larger nodes
            var spacingY = 250; // Taller spacing for larger nodes
            
            for (var i = 0; i < count; i++)
            {
                var row = i / cols;
                var col = i % cols;
                
                var x = startX + col * spacingX;
                var y = startY + row * spacingY;
                
                positions.Add((x, y));
            }
            
            return positions;
        }

        private static string GetTitleStyle()
        {
            return "text;html=1;strokeColor=none;fillColor=none;align=center;verticalAlign=middle;" +
                   "whiteSpace=wrap;rounded=0;fontSize=20;fontStyle=1;fontColor=#0366d6;";
        }

        private static string GetEnhancedStyle(Structure structure)
        {
            var baseStyle = "rounded=1;whiteSpace=wrap;html=1;arcSize=8;strokeWidth=2;" +
                           "align=left;verticalAlign=top;spacingTop=10;spacingLeft=10;spacingRight=10;";
            
            if (structure is Person)
            {
                return baseStyle + "fillColor=#f6f8fa;fontColor=#24292e;strokeColor=#d1d5da;";
            }
            else
            {
                return baseStyle + "fillColor=#ffffff;fontColor=#24292e;strokeColor=#0366d6;";
            }
        }

        private static string GetEnhancedEdgeStyle()
        {
            return "edgeStyle=orthogonalEdgeStyle;rounded=1;orthogonalLoop=1;jettySize=auto;html=1;" +
                   "strokeWidth=2;strokeColor=#0366d6;fontColor=#586069;fontSize=11;" +
                   "labelBackgroundColor=#ffffff;labelBorderColor=#d1d5da;";
        }
    }
}
