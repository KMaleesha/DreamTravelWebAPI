using System;
using System.Collections.Generic;
using MongoDB.Driver;
using DreamTravelWebAPI.Models;

namespace DreamTravelWebAPI.Services
{
    public class TrainService : ITrainService
    {
        private readonly IMongoCollection<Train> _trains;
        private readonly IBookingService _bookingService;

        public TrainService(MongoDBSettings settings, IBookingService bookingService)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _trains = database.GetCollection<Train>("Trains");
            _bookingService = bookingService;
        }
        public bool Exists(string id) => _trains.CountDocuments(train => train.Id == id) > 0;

        public List<Train> GetAll() => _trains.Find(train => true).ToList();

        public Train GetById(string id) => _trains.Find<Train>(train => train.Id == id).FirstOrDefault();

        public Train Create(Train train)
        {
            if (Exists(train.Id))
            {
                throw new InvalidOperationException("A train with the same ID already exists.");
            }

            _trains.InsertOne(train);
            return train;
        }

        public void DeactivateTrain(string trainId)
        {
            var reservations = _bookingService.GetBookingsForTrain(trainId);
            if (reservations.Count > 0)
            {
                throw new InvalidOperationException("Cannot deactivate the train as there are active reservations.");
            }

            var train = GetById(trainId);
            if (train == null)
            {
                throw new Exception("Train not found.");
            }

            train.IsPublished = false;
            Update(trainId, train);
        }

        public void ActiveTrain(string trainId)
        {
            var train = GetById(trainId);
            if (train == null)
            {
                throw new Exception("Train not found.");
            }

            train.IsPublished = true;
            Update(trainId, train);
        }

        public void Update(string id, Train trainIn)
        {
            _trains.ReplaceOne(train => train.Id == id, trainIn);
        }

        public void Delete(string id) => _trains.DeleteOne(train => train.Id == id);

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
