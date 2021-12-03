using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
namespace dotnetDockerHerokuTwilio
{
    // checkout 52 min of https://www.youtube.com/watch?v=Pi46L7UYP8I to see more about defining models
        public class OpenMicNight : DbContext // do you need this??
    {
        public string HostPhoneNumber { get; set; }
        public string VenueName { get; set; }
        public string RoomCode { get; set; }
        public virtual List<Performance> Performances { get; set; }
        /*
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        */
    }
}
