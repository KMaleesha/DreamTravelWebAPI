using MongoDB.Bson.Serialization.Attributes; // Imports MongoDB attributes used for BSON serialization.
using MongoDB.Bson; // Imports MongoDB's Bson data types.
using DreamTravelWebAPI.Models; // Imports the models used by the application.

public class Train
{
    [BsonId] // Marks the property as the primary key for the MongoDB document.
    [BsonRepresentation(BsonType.ObjectId)] // Represents the Id as an ObjectId in MongoDB.
    public String Id { get; set; } // Unique identifier for the train document in MongoDB.

    [BsonElement("Name")] // Maps the property to the "Name" field in the MongoDB document.
    public string Name { get; set; } // Name of the train.

    [BsonElement("IsPublished")] // Maps the property to the "IsPublished" field in the MongoDB document.
    public bool IsPublished { get; set; } // Indicates whether the train is published or not.
}
