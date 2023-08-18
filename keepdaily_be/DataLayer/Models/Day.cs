namespace DataLayer.Models
{
    public class Day
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Date { get; set; }
        public string ImgUrl { get; set; } = null!;
        public int PlanId { get; set; }
    }
}
