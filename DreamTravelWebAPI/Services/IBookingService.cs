using DreamTravelWebAPI.Models;
using System.Collections.Generic;

namespace DreamTravelWebAPI.Services
{
    public interface IBookingService
    {
        List<Booking> GetAll();
        Booking GetByBookingID(string bookingID);
        IEnumerable<Booking> GetByNIC(string NIC);
        Booking Create(Booking booking);
        void Update(string bookingID, Booking booking);
        void UpdateStatus(string bookingID, Booking.StatusType status);
        void Delete(string bookingID);
        bool Exists(string bookingID);
    }
}
