using System.Collections.Generic;
using DreamTravelWebAPI.Models;

// Defines the contract for Schedule related services within the application.
public interface IScheduleService
{
    // Creates a new schedule associated with a specific train.
    Schedule CreateScheduleWithTrainDetails(string trainId, Schedule schedule);

    // Retrieves a schedule by its unique identifier.
    Schedule GetScheduleById(int scheduleId);

    // Retrieves all schedules associated with a specific train.
    IEnumerable<Schedule> GetSchedulesByTrainId(string trainId);

    // Updates a specific train schedule with new details.
    // Returns 'true' if the update was successful, 'false' otherwise.
    bool UpdateExistingTrainSchedule(int scheduleId, Schedule updatedSchedule);

    // Cancels a specific train reservation/schedule.
    // Returns 'true' if the cancellation was successful, 'false' otherwise.
    bool CancelTrainReservation(int scheduleId);

    // Retrieves all train schedules.
    IEnumerable<Schedule> GetAllSchedules();

    // Retrieves all schedules of trains that are marked as published.
    IEnumerable<Schedule> GetSchedulesOfPublishedTrains();
}
