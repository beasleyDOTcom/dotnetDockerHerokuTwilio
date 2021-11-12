using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetDockerHerokuTwilio
{
    public class OpenMicNight
    {
        public string OpenMicName { get; set; }
        public string RoomCode { get; set; }
        public List<Performance> Performances { get; set; }
        /*
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        */
    }
}
