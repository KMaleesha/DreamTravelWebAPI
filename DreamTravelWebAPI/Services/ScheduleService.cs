using System;
using System.Collections.Generic;
using DreamTravelWebAPI;
using DreamTravelWebAPI.Models;
using MongoDB.Driver;

public class ScheduleService : IScheduleService
{
    private readonly IMongoCollection<Schedule> _schedules;
    private readonly IMongoCollection<Train> _trains;

    public ScheduleService(MongoDBSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        _schedules = database.GetCollection<Schedule>("Schedules");
        _trains = database.GetCollection<Train>("Trains");
    }

    public Schedule CreateScheduleWithTrainDetails(string trainId, Schedule schedule)
    {
        var train = _trains.Find(t => t.Id == trainId && t.IsPublished).FirstOrDefault();
        if (train == null)
        {
            throw new Exception("Train not found or not eligible for scheduling.");
        }

        schedule.Train = train;
        _schedules.InsertOne(schedule);
        return schedule;
    }

    public Schedule GetScheduleById(int scheduleId)
    {
        return _schedules.Find(s => s.Id == scheduleId).FirstOrDefault();
    }

    public IEnumerable<Schedule> GetSchedulesByTrainId(String trainId)
    {
        return _schedules.Find(s => s.Train.Id == trainId).ToList();
    }

    public bool UpdateExistingTrainSchedule(int scheduleId, Schedule updatedSchedule)
    {
        var existingSchedule = GetScheduleById(scheduleId);
        if (existingSchedule == null)
        {
            throw new Exception("Schedule not found.");
        }

        // Check if the updated schedule is valid (e.g., train is published)
        var train = _trains.Find(t => t.Id == existingSchedule.Train.Id && t.IsPublished).FirstOrDefault();
        if (train == null)
        {
            throw new Exception("Train not found or not eligible for scheduling.");
        }

        updatedSchedule.Id = scheduleId;
        updatedSchedule.Train = train;

        var result = _schedules.ReplaceOne(s => s.Id == scheduleId, updatedSchedule);
        return result.ModifiedCount > 0;
    }

    public bool CancelTrainReservation(int scheduleId)
    {
        var schedule = GetScheduleById(scheduleId);
        if (schedule == null)
        {
            throw new Exception("Schedule not found.");
        }

        // Check if there are existing reservations (implement your logic here)
        if (HasExistingReservations(schedule))
        {
            throw new Exception("Cannot cancel a train with existing reservations.");
        }

        // Implement your cancellation logic here
        // ...

        return true; // Placeholder
    }

    // Helper method to check for existing reservations
    private bool HasExistingReservations(Schedule schedule)
    {
        // Implement your logic to check for existing reservations
        // Return true if there are reservations, false otherwise
        // ...

        return false; // Placeholder
    }

    public IEnumerable<Schedule> GetAllSchedules()
    {
        return _schedules.Find(_ => true).ToList();
    }
}
