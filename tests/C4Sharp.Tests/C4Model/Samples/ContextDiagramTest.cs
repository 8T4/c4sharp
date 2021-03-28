using System.IO;
using C4Sharp.Models;
using C4Sharp.Models.Diagrams;
using C4Sharp.Models.Plantuml;
using Xunit;
using static C4Sharp.Models.Relationships.Position;
using static C4Sharp.Tests.C4Model.Persons;
using static C4Sharp.Tests.C4Model.Systems;

namespace C4Sharp.Tests.C4Model.Samples
{
    public class ContextDiagramTest
    {
        [Fact]
        public void Its_C4_Model_Context_Diagram()
        {
            var diagram = new ContextDiagram
            {
                Title = "System Context diagram for Internet Banking System",
                Structures = new Structure[]
                {
                    Customer,
                    BankingSystem,
                    Mainframe,
                    MailSystem
                },
                Relationships = new []
                {
                    Customer > BankingSystem,
                    (Customer < MailSystem)["Sends e-mails to"],
                    (BankingSystem > MailSystem)["Sends e-mails", "SMTP"][Neighbor],
                    BankingSystem > Mainframe,
                }
            };

            PlantumlFile.Save(diagram);
            PlantumlFile.ExportToPng(diagram);
            
            Assert.True(File.Exists($"c4/{diagram.Slug()}.puml"));
        }        
    }
}