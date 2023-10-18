using MongoDB.Bson.Serialization.Attributes; // Imports MongoDB attributes used for BSON serialization.
using MongoDB.Bson; // Imports MongoDB's Bson data types.
using DreamTravelWebAPI.Models; // Imports the models used by the application.

public class Train
{
    public String Id { get; set; }
    public string Name { get; set; }
    public bool IsPublished { get; set; }
}