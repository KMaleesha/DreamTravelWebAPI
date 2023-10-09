using DreamTravelWebAPI.Models;
using System.Collections.Generic;

public interface IScheduleService
{
    Schedule Create(int trainId, Schedule schedule);
    IEnumerable<Schedule> GetByTrainId(int trainId);
}
