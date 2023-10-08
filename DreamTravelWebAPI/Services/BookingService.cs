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

        public Booking Create(Booking booking)
        {
            _bookings.InsertOne(booking);
            return booking;
        }

        public void Update(string bookingID, Booking bookingIn)
        {
            _bookings.ReplaceOne(booking => booking.BookingID == bookingID, bookingIn);
        }

        public void UpdateStatus(string bookingID, Booking.StatusType status)
        {
            var booking = GetByBookingID(bookingID) ?? throw new Exception("Booking not found.");
            booking.Status = status;
            Update(bookingID, booking);
        }

        public void Delete(string bookingID) => _bookings.DeleteOne(booking => booking.BookingID == bookingID);

        public bool Exists(string bookingID) => _bookings.CountDocuments(booking => booking.BookingID == bookingID) > 0;
    }
}
