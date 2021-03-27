using System.IO;
using C4Sharp.Models;
using C4Sharp.Models.Diagrams;
using C4Sharp.Models.Puml;
using C4Sharp.Models.Relationships;
using Xunit;
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
                Relationships = new Relationship[]
                {
                    new Relationship(Customer, BankingSystem, "Uses"),
                    new RelateBack(Customer, MailSystem, "Sends e-mails to"),
                    new RelateNeighbor(BankingSystem, MailSystem, "Sends e-mails", "SMTP"),
                    new Relationship(BankingSystem, Mainframe, "Uses"),
                }
            };

            PumlFile.Save(diagram);
            PumlFile.ExportToPng(diagram);
            
            Assert.True(File.Exists($"c4/{diagram.Slug()}.puml"));
        }        
    }
}