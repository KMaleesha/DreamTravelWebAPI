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

        public Train CreateTrain(Train train)
        {
            if (train.IsPublished)
            {
                _trains.InsertOne(train);
                return train;
            }
            throw new InvalidOperationException("Train can only be created if it is published.");
        }

        public List<Train> GetByIsPublished(bool isPublished)
        {
            return _trains.Find(train => train.IsPublished == isPublished).ToList();
        }

        public Train GetTrainById(string trainId)
        {
            var filter = Builders<Train>.Filter.Eq(train => train.Id, trainId);
            return _trains.Find(filter).FirstOrDefault();
        }
    }
}
