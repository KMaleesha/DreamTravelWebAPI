using DreamTravelWebAPI.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DreamTravelWebAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly IMongoCollection<Booking> _bookings;

        public BookingService(MongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _bookings = database.GetCollection<Booking>("Bookings"); // Corrected collection name from "Users" to "Bookings"
        }

        public List<Booking> GetAll() => _bookings.Find(booking => true).ToList();

        public Booking GetByBookingID(string bookingID) => _bookings.Find<Booking>(booking => booking.BookingID == bookingID).FirstOrDefault();
        // public List<Booking> GetByNIC(string NIC) => _bookings.Find<Booking>(booking => booking.NIC == NIC).ToList();
        public IEnumerable<Booking> GetByNIC(string NIC) => _bookings.Find<Booking>(booking => booking.NIC == NIC).ToList();


        public Booking Create(Booking booking)
        {
            _bookings.InsertOne(booking);
            return booking;
        }

        public void Update(string bookingID, Booking bookingIn)
        {
            var originalBooking = GetByBookingID(bookingID);
            if (originalBooking == null)
            {
                throw new Exception("Booking not found.");
            }

            bookingIn.Id = originalBooking.Id; // Ensure ObjectId consistency

            var filter = Builders<Booking>.Filter.Eq("BookingID", bookingID);
            _bookings.ReplaceOne(filter, bookingIn);
        }


        public void UpdateStatus(string bookingID, Booking.StatusType status)
        {
            var booking = GetByBookingID(bookingID);
            if (booking == null)
            {
                throw new Exception($"Booking with ID {bookingID} not found.");
            }
            booking.Status = status;
            Update(bookingID, booking);
        }


        public void Delete(string bookingID)
        {
            var originalBooking = GetByBookingID(bookingID);
            var filter = Builders<Booking>.Filter.Eq("Id", originalBooking.Id);
            _bookings.DeleteOne(filter);
        }


        public bool Exists(string bookingID)
        {
            var filter = Builders<Booking>.Filter.Eq("BookingID", bookingID);
            return _bookings.CountDocuments(filter) > 0;
        }
    }
}
