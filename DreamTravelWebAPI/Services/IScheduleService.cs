using System.Collections.Generic;
using DreamTravelWebAPI.Models;

public interface IScheduleService
{
    Schedule CreateScheduleWithTrainDetails(string trainId, Schedule schedule);
    Schedule GetScheduleById(int scheduleId);
    IEnumerable<Schedule> GetSchedulesByTrainId(string trainId);
    bool UpdateExistingTrainSchedule(int scheduleId, Schedule updatedSchedule);
    bool CancelTrainReservation(int scheduleId);
}
