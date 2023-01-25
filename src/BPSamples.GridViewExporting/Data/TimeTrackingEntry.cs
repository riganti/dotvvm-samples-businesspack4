using System;
using System.Threading.Tasks;

namespace BPSamples.GridViewExporting.Data
{
    public class TimeTrackingEntry
    {
        public int Id { get; set; }
        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public double Hours => (EndDate - BeginDate).TotalHours;

        public string EmployeeName { get; set; }

        public string Description { get; set; }
    }
}
