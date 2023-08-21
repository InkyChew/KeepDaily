namespace DomainLayer.Models
{
    public class LineAuthRes
    {
        public string? Code { get; set; }
        public string State { get; set; } = null!;
        public string? Error { get; set; }
        public string? Error_description { get; set; }
    }
}
