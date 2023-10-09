namespace DreamTravelWebAPI.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public int TrainId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        
    }

}
