// --------------------------------------------------------------
// Project: DreamTravelWebAPI
// Class: Booking
// Author: Wijesooriya W.M.R.K
// Created: 10/13/2023
// Description: Model for bookings in the Dream Travel Web API
// --------------------------------------------------------------
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DreamTravelWebAPI.Models
{
    public class Booking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string BookingID { get; set; }

        public string ScheduleID { get; set; }
        public int SeatCount { get; set; }
        public string NIC { get; set; }  // NIC of the traveler making the booking
        public string TrainID { get; set; }  // Identifier for the train being booked
        public DateTime ReservationDate { get; set; }
        public DateTime BookingDate { get; set; }
        public string ReferenceID { get; set; }
        public enum StatusType
        {
            Reserved,
            Canceled
        }
        [BsonElement("Status")]
        [System.ComponentModel.Description("Status of the booking. Either 'Reserved' or 'Canceled'.")]
        public Booking.StatusType Status { get; set; }
 
    }
}
