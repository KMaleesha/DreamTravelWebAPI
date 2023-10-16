// --------------------------------------------------------------
// Project: DreamTravelWebAPI
// Class: BookingsController
// Author: Wijesooriya W.M.R.K
// Created: 10/13/2023
// Description: Controller for managing bookings in the Dream Travel Web API
// --------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using DreamTravelWebAPI.Models;
using DreamTravelWebAPI.Services;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace DreamTravelWebAPI.Controllers
{
    // Requires the user to be authenticated
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        // Constructor
        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // Retrieves all bookings
        [HttpGet]
        public IActionResult GetAllBookings()
        {
            return Ok(_bookingService.GetAll());
        }

        // Retrieves a specific booking by bookingID
        [HttpGet("{bookingID}")]
        public IActionResult GetBooking(string bookingID)
        {
            var booking = _bookingService.GetByBookingID(bookingID);
            if (booking == null)
                return NotFound("Booking not found");
            return Ok(booking);
        }

        // Retrieves bookings by NIC
        [HttpGet("nic/{nic}")]
        public IActionResult GetBookingByNic(string nic)
        {
            var bookings = _bookingService.GetByNIC(nic);
            if (!bookings.Any())
                return NotFound("Booking not found");
            return Ok(bookings);
        }

        // Creates a new booking
        [HttpPost]
        public IActionResult CreateBooking([FromBody] Booking booking)
        {
            if (_bookingService.Exists(booking.BookingID))
                return BadRequest("Booking with the provided BookingID already exists");

            _bookingService.Create(booking);
            return Ok("Booking created successfully");
        }

        // Updates an existing booking by bookingID
        [HttpPut("{bookingID}")]
        public IActionResult UpdateBooking(string bookingID, [FromBody] Booking updatedBooking)
        {
            var existingBooking = _bookingService.GetByBookingID(bookingID);
            if (existingBooking == null)
                return NotFound("Booking not found");

            _bookingService.Update(bookingID, updatedBooking);
            return Ok("Booking updated successfully");
        }

        // Updates the status of an existing booking
        [HttpPatch("{bookingID}/status")]
        public IActionResult UpdateBookingStatus(string bookingID, [FromBody] BookingStatusUpdateDTO statusUpdate)
        {
            try
            {
                _bookingService.UpdateStatus(bookingID, statusUpdate.Status);
                return Ok("Booking status updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Deletes an existing booking by bookingID
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
