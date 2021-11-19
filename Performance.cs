using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetDockerHerokuTwilio
{
    public class Performance
    {
        public virtual List<Performer> Performers { get; set; }
        public string GroupAnnouncedAs { get; set; }
        public string PerformanceContactName { get; set; }
        public string PerformanceContactNumber { get; set; }
        /*
        public int TileNumber { get; set; }public List<string> PerformersNeeds { get; set; } 
        public List<string> InstrumentsInvolved { get; set; }
        public List<int> NumberOfVocalMicsRequired { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime {get; set;}
        */
    }
}
