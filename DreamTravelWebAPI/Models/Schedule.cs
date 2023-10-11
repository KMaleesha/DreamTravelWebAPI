using System;

namespace DreamTravelWebAPI.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string StartStation { get; set; }
        public string StoppingStation { get; set; }
        public Train Train { get; set; } 

    }
}
