namespace C4Sharp.Models.Plantuml
{
    /// <summary>
    /// PlantUM Result
    /// </summary>    
    public readonly struct PlantumlResult
    {
        public bool Success { get; }
        public string Messages { get; }

        public PlantumlResult(bool success, string messages)
        {
            Success = success;
            Messages = messages;
        }
    }
}