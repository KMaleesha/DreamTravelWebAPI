using System;

namespace DreamTravelWebAPI.Models
{
    // Represents a train's schedule within the application.
    public class Schedule
    {
        // Unique identifier for the schedule.
        public int Id { get; set; }

        // Time when the train is scheduled to depart.
        public String DepartureTime { get; set; }

        // Time when the train is scheduled to arrive at its destination.
        public String ArrivalTime { get; set; }

        // The station where the train journey begins.
        public string StartStation { get; set; }

        // The station where the train makes a stop or ends its journey.
        public string StoppingStation { get; set; }

        // Associated train details with this schedule.
        public Train Train { get; set; }
    }
}
