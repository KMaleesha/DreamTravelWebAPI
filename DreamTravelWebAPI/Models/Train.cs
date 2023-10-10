using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using DreamTravelWebAPI.Models;

public class Train
{
    public String Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public bool IsPublished { get; set; }
    public List<Schedule> Schedules { get; set; } // List of schedules for the train
}

