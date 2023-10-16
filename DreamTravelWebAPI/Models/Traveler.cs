// --------------------------------------------------------------
// Project: DreamTravelWebAPI
// Class: Traveler
// Author: Wijesooriya W.M.R.K
// Created: 10/13/2023
// Description: Model for travelers in the Dream Travel Web API
// --------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace DreamTravelWebAPI.Models
{
    public class Traveler
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string NIC { get; set; }  // Using NIC as the unique identifier for travelers

        public string Email { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; }
    }

}


