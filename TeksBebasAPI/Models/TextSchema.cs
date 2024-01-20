namespace TeksBebasAPI.Models
{
    public class TextSchema
    {
        public required string Text { get; set; }
    }

    public class TextResponseSchema
    {
        public string? Text { get; set; }
        public string? Label { get; set; }
        public Dictionary<string, float>? Score { get; set; }
    }
}
