namespace FinalProject.Models.Entities
{
    public class Message
    {
        public string Subject { get; set; } = null!;
        public string? Body { get; set; }
        public string To { get; set; } = null!;
        public string? Name { get; set; }
        public bool IsHtml { get; set; }
    }
}
