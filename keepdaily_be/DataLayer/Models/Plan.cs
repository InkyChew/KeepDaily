namespace DataLayer.Models
{
    public class Plan
    {
        public int id { get; set; }
        public string title { get; set; } = null!;
        public string description { get; set; } = null!;
        public DateTime StartFrom { get; set; }
        public List<Day> DayList { get; set; } = new List<Day>();
    }
}
