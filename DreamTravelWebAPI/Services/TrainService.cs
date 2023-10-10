using System;
using System.Collections.Generic;
using MongoDB.Driver;
using DreamTravelWebAPI.Models;

namespace DreamTravelWebAPI.Services
{
    public class TrainService : ITrainService
    {
        private readonly IMongoCollection<Train> _trains;

        public TrainService(MongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _trains = database.GetCollection<Train>("Trains");
        }

        public List<Train> GetAll() => _trains.Find(train => true).ToList();

        public Train GetById(string id) => _trains.Find<Train>(train => train.Id == id).FirstOrDefault();

        public Train Create(Train train)
        {
            _trains.InsertOne(train);
            return train;
        }

        public void Update(string id, Train trainIn)
        {
            _trains.ReplaceOne(train => train.Id == id, trainIn);
        }

        public void Delete(string id) => _trains.DeleteOne(train => train.Id == id);

        public bool Exists(string id) => _trains.CountDocuments(train => train.Id == id) > 0;

        public void Activate(string id)
        {
            var train = GetById(id) ?? throw new Exception("Train not found.");
            if (train.IsActive)
            {
                throw new Exception("Train is already activated.");
            }

            train.IsActive = true;
            Update(id, train);
        }

        public void Deactivate(string id)
        {
            var train = GetById(id);
            if (train == null)
            {
                throw new Exception("Train not found.");
            }
            if (!train.IsActive)
            {
                throw new Exception("Train is already deactivated.");
            }
            train.IsActive = false;
            Update(id, train);
        }

        public Train CreateTrain(Train train)
        {
            // Check if the train is active and published before creating it
            if (train.IsActive && train.IsPublished)
            {
                _trains.InsertOne(train); // Insert the train into the MongoDB collection
                return train;
            }
            throw new InvalidOperationException("Train can only be created if it is active and published.");
        }


        public Train GetTrainById(string trainId)
        {
            var filter = Builders<Train>.Filter.Eq(train => train.Id, trainId);
            return _trains.Find(filter).FirstOrDefault();
        }
    }
}
