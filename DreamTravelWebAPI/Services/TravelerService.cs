// --------------------------------------------------------------
// Project: DreamTravelWebAPI
// Class: TravelerService
// Author: Wijesooriya W.M.R.K
// Created: 10/13/2023
// Description: Service for managing travelers in the Dream Travel Web API
// --------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using DreamTravelWebAPI.Models;
using MongoDB.Driver;

namespace DreamTravelWebAPI.Services
{
    public class TravelerService : ITravelerService
    {
        private readonly IMongoCollection<Traveler> _travelers;

        // Constructor: Initializes MongoDB settings and collection
        public TravelerService(MongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _travelers = database.GetCollection<Traveler>("Travelers");
        }

        // Fetches all travelers
        public List<Traveler> GetAll() => _travelers.Find(traveler => true).ToList();

        // Fetches a traveler by NIC
        public Traveler GetByNic(string nic) => _travelers.Find<Traveler>(traveler => traveler.NIC == nic).FirstOrDefault();

        // Creates a new traveler
        public Traveler Create(Traveler traveler)
        {
            _travelers.InsertOne(traveler);
            return traveler;
        }

        // Updates an existing traveler by NIC
        public void Update(string nic, Traveler travelerIn)
        {
            _travelers.ReplaceOne(traveler => traveler.NIC == nic, travelerIn);
        }

        // Deletes a traveler by NIC
        public void Delete(string nic) => _travelers.DeleteOne(traveler => traveler.NIC == nic);

        // Checks if a traveler exists by NIC
        public bool Exists(string nic) => _travelers.CountDocuments(traveler => traveler.NIC == nic) > 0;

        // Activates a traveler by NIC
        public void Activate(string nic)
        {
            var traveler = GetByNic(nic) ?? throw new Exception("Traveler not found.");
            if (traveler.IsActive)
            {
                throw new Exception("Traveler is already activated.");
            }

            traveler.IsActive = true;
            Update(nic, traveler);
        }

        // Deactivates a traveler by NIC
        public void Deactivate(string nic)
        {
            var traveler = GetByNic(nic);
            if (traveler == null)
            {
                throw new Exception("Traveler not found.");
            }
            if (traveler.IsActive == false)
            {
                throw new Exception("Traveler is already deactivated.");
            }
            traveler.IsActive = false;
            Update(nic, traveler);
        }
    }
}
