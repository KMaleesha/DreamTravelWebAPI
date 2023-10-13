using System;
using System.Collections.Generic;
using DreamTravelWebAPI;
using DreamTravelWebAPI.Models;
using MongoDB.Driver;

public class ScheduleService : IScheduleService
{
    private readonly IMongoCollection<Schedule> _schedules;
    private readonly IMongoCollection<Train> _trains;

    // Constructor initializes the MongoDB collections for schedules and trains.
    public ScheduleService(MongoDBSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        _schedules = database.GetCollection<Schedule>("Schedules");
        _trains = database.GetCollection<Train>("Trains");
    }

    // Method to create a schedule with train details.
    public Schedule CreateScheduleWithTrainDetails(string trainId, Schedule schedule)
    {
        // Check if a schedule with the same ID already exists.
        var existingSchedule = _schedules.Find(s => s.Id == schedule.Id).FirstOrDefault();
        if (existingSchedule != null)
        {
            throw new ArgumentException($"A schedule with the ID {schedule.Id} already exists.");
        }

        // Check if the associated train exists.
        var train = _trains.Find(t => t.Id == trainId).FirstOrDefault();
        if (train == null)
        {
            throw new Exception("Train not found.");
        }

        // Validate mandatory fields in the schedule.
        if (string.IsNullOrEmpty(schedule.DepartureTime) ||
            string.IsNullOrEmpty(schedule.ArrivalTime) ||
            string.IsNullOrEmpty(schedule.StartStation) ||
            string.IsNullOrEmpty(schedule.StoppingStation) ||
            schedule.Train == null)
        {
            throw new ArgumentException("All fields of the schedule must be provided.");
        }

        // Assign the train to the schedule and insert into the database.
        schedule.Train = train;
        _schedules.InsertOne(schedule);
        return schedule;
    }

    // Method to update an existing train schedule.
    public bool UpdateExistingTrainSchedule(int scheduleId, Schedule updatedSchedule)
    {
        // Check if the schedule exists.
        var existingSchedule = GetScheduleById(scheduleId);
        if (existingSchedule == null)
        {
            throw new Exception("Schedule not found.");
        }

        // Ensure the associated train exists.
        var train = _trains.Find(t => t.Id == existingSchedule.Train.Id).FirstOrDefault();
        if (train == null)
        {
            throw new Exception("Train not found.");
        }

        // Assign updated values and replace the existing schedule in the database.
        updatedSchedule.Id = scheduleId;
        updatedSchedule.Train = train;

        var result = _schedules.ReplaceOne(s => s.Id == scheduleId, updatedSchedule);
        return result.ModifiedCount > 0;
    }

    // Retrieve a schedule based on its ID.
    public Schedule GetScheduleById(int scheduleId)
    {
        return _schedules.Find(s => s.Id == scheduleId).FirstOrDefault();
    }

    // Retrieve all schedules associated with a specific train.
    public IEnumerable<Schedule> GetSchedulesByTrainId(String trainId)
    {
        return _schedules.Find(s => s.Train.Id == trainId).ToList();
    }

    // Cancel a specific train reservation.
    public bool CancelTrainReservation(int scheduleId)
    {
        var schedule = GetScheduleById(scheduleId);
        if (schedule == null)
        {
            throw new Exception("Schedule not found.");
        }

        // Ensure there are no existing reservations for the train.
        if (HasExistingReservations(schedule))
        {
            throw new Exception("Cannot cancel a train with existing reservations.");
        }

        // Logic to cancel the reservation can be implemented here.

        return true;
    }

    // Helper method to check if a train has existing reservations.
    private bool HasExistingReservations(Schedule schedule)
    {
        // Logic to check for reservations can be implemented here.

        return false; // Placeholder value.
    }

    // Retrieve all available schedules.
    public IEnumerable<Schedule> GetAllSchedules()
    {
        return _schedules.Find(_ => true).ToList();
    }

    // Retrieve schedules for all published trains.
    public IEnumerable<Schedule> GetSchedulesOfPublishedTrains()
    {
        return _schedules.Find(s => s.Train.IsPublished).ToList();
    }
}