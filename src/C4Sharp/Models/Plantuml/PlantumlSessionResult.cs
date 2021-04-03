namespace C4Sharp.Models.Plantuml
{
    public class PlantumlSessionResult
    {
        public bool Success { get; private set; }
        public string Messages { get; private set; }

        public PlantumlSessionResult(bool success, string messages)
        {
            Success = success;
            Messages = messages;
        }
    }
}