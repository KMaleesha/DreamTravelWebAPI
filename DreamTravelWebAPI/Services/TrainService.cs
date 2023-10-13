using System;
using System.Collections.Generic;
using MongoDB.Driver;
using DreamTravelWebAPI.Models;

namespace DreamTravelWebAPI.Services
{
    // This service provides methods to manage train-related operations.
    public class TrainService : ITrainService
    {
        // MongoDB collection for trains.
        private readonly IMongoCollection<Train> _trains;
        
        // Reference to the booking service to handle train booking-related operations.
        private readonly IBookingService _bookingService;

        // Constructor initializes the MongoDB collection for trains and injects the booking service.
        public TrainService(MongoDBSettings settings, IBookingService bookingService)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _trains = database.GetCollection<Train>("Trains");
            _bookingService = bookingService;
        }

        // Method to check if a train with the specified ID exists in the database.
        public bool Exists(string id) => _trains.CountDocuments(train => train.Id == id) > 0;

        // Method to retrieve all trains from the database.
        public List<Train> GetAll() => _trains.Find(train => true).ToList();

        // Retrieves a train from the database using its ID.
        public Train GetById(string id) => _trains.Find<Train>(train => train.Id == id).FirstOrDefault();

        // Method to create and insert a new train into the database.
        public Train Create(Train train)
        {
            // Check if a train with the same ID already exists in the database.
            if (Exists(train.Id))
            {
                throw new InvalidOperationException("A train with the same ID already exists.");
            }

            // Insert the train into the database.
            _trains.InsertOne(train);
            return train;
        }

        // Deactivates a specific train by setting its 'IsPublished' property to false.
        public void DeactivateTrain(string trainId)
        {
            // Check for any existing reservations for the train.
            var reservations = _bookingService.GetBookingsForTrain(trainId);
            if (reservations.Count > 0)
            {
                throw new InvalidOperationException("Cannot deactivate the train as there are active reservations.");
            }

            // Retrieve the train using its ID.
            var train = GetById(trainId);
            if (train == null)
            {
                throw new Exception("Train not found.");
            }

            // Update the 'IsPublished' status of the train.
            train.IsPublished = false;
            Update(trainId, train);
        }

        // Activates a specific train by setting its 'IsPublished' property to true.
        public void ActiveTrain(string trainId)
        {
            // Retrieve the train using its ID.
            var train = GetById(trainId);
            if (train == null)
            {
                throw new Exception("Train not found.");
            }

            // Update the 'IsPublished' status of the train.
            train.IsPublished = true;
            Update(trainId, train);
        }

        // Replaces an existing train record in the database with an updated version.
        public void Update(string id, Train trainIn)
        {
            _trains.ReplaceOne(train => train.Id == id, trainIn);
        }

        // Deletes a specific train from the database using its ID.
        public void Delete(string id) => _trains.DeleteOne(train => train.Id == id);

        // Retrieves a list of trains based on their 'IsPublished' status.
        public List<Train> GetByIsPublished(bool isPublished)
        {
            return _trains.Find(train => train.IsPublished == isPublished).ToList();
        }

        // Retrieves a train using a filter based on its ID.
        public Train GetTrainById(string trainId)
        {
            var filter = Builders<Train>.Filter.Eq(train => train.Id, trainId);
            return _trains.Find(filter).FirstOrDefault();
        }

    }
}
