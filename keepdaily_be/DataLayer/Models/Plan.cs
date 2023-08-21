namespace DataLayer.Models
{
    public class Plan
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartFrom { get; set; }
        public List<Day> Days { get; set; } = new List<Day>();
    }
}
