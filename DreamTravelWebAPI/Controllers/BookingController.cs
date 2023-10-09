using Microsoft.AspNetCore.Mvc;
using DreamTravelWebAPI.Models;
using DreamTravelWebAPI.Services;
using System;
using Microsoft.AspNetCore.Authorization;

namespace DreamTravelWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // GET: api/Bookings
        [HttpGet]
        public IActionResult GetAllBookings()
        {
            return Ok(_bookingService.GetAll());
        }

        // GET: api/Bookings/{bookingID}
        [HttpGet("{bookingID}")]
        public IActionResult GetBooking(string bookingID)
        {
            var booking = _bookingService.GetByBookingID(bookingID);
            if (booking == null)
                return NotFound("Booking not found");
            return Ok(booking);
        }

        // POST: api/Bookings
        [HttpPost]
        public IActionResult CreateBooking([FromBody] Booking booking)
        {
            if (_bookingService.Exists(booking.BookingID))
                return BadRequest("Booking with the provided BookingID already exists");

            _bookingService.Create(booking);
            return Ok("Booking created successfully");
        }

        // PUT: api/Bookings/{bookingID}
        [HttpPut("{bookingID}")]
        public IActionResult UpdateBooking(string bookingID, [FromBody] Booking updatedBooking)
        {
            var existingBooking = _bookingService.GetByBookingID(bookingID);
            if (existingBooking == null)
                return NotFound("Booking not found");

            _bookingService.Update(bookingID, updatedBooking);
            return Ok("Booking updated successfully");
        }

        // PATCH: api/Bookings/{bookingID}/status
        [HttpPatch("{bookingID}/status")]
        public IActionResult UpdateBookingStatus(string bookingID, [FromBody] Booking.StatusType status)
        {
            try
            {
                _bookingService.UpdateStatus(bookingID, status);
                return Ok("Booking status updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Bookings/{bookingID}
        [HttpDelete("{bookingID}")]
        public IActionResult DeleteBooking(string bookingID)
        {
            if (!_bookingService.Exists(bookingID))
                return NotFound("Booking not found");

            _bookingService.Delete(bookingID);
            return Ok("Booking deleted successfully");
        }
    }
}
