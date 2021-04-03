using System;
using C4Sharp.Models.Diagrams;

namespace C4Sharp.Models.Plantuml
{
    public class PlantumlSession: IDisposable
    {
        public string FilePath { get; }

        public PlantumlSession()
        {
            FilePath = PlantumlStream.LoadPlantUmlEngine();
        }

        public void Dispose()
        {
            PlantumlStream.RemovePlantUmlEngine(FilePath);
        }
    }
}