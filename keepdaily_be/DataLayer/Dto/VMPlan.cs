using DataLayer.Models;

namespace DataLayer.Dto
{
    public class VMPlan
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartFrom { get; set; }
        public List<VMPlanCalendar> Calendars { get; set; } = new List<VMPlanCalendar>();
    }
}
