using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dto
{
    public class VMPlanCalendar
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public List<VMPlanDay> Days { get; set; } = new List<VMPlanDay>();
    }
}
