using System;

namespace DreamTravelWebAPI.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public String DepartureTime { get; set; }
        public String ArrivalTime { get; set; }
        public string StartStation { get; set; }
        public string StoppingStation { get; set; }
        public Train Train { get; set; } 

    }
}
