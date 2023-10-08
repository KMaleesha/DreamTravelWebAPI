using System.Collections.Generic;
using System.Linq;
using DreamTravelWebAPI.Models;
using MongoDB.Driver;

namespace DreamTravelWebAPI.Services
{
    public class TravelerService : ITravelerService
    {

        private readonly IMongoCollection<Traveler> _travelers;

        public TravelerService(MongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _travelers = database.GetCollection<Traveler>("Travelers");
        }

        public List<Traveler> GetAll() => _travelers.Find(traveler => true).ToList();

        public Traveler GetByNic(string nic) => _travelers.Find<Traveler>(traveler => traveler.NIC == nic).FirstOrDefault();

        public Traveler Create(Traveler traveler)
        {
            _travelers.InsertOne(traveler);
            return traveler;
        }

        public void Update(string nic, Traveler travelerIn)
        {
            _travelers.ReplaceOne(traveler => traveler.NIC == nic, travelerIn);
        }

        public void Delete(string nic) => _travelers.DeleteOne(traveler => traveler.NIC == nic);

        public bool Exists(string nic) => _travelers.CountDocuments(traveler => traveler.NIC == nic) > 0;

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

        // Deactivate a traveler
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
