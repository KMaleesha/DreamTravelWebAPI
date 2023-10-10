using DreamTravelWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class ScheduleService : IScheduleService
{
    private static List<Schedule> schedules = new List<Schedule>();

    public Schedule Create(int trainId, Schedule schedule)
    {
        schedule.TrainId = trainId;
        schedules.Add(schedule);
        return schedule;
    }

    public IEnumerable<Schedule> GetByTrainId(int trainId)
    {
        return schedules.Where(s => s.TrainId == trainId).ToList();
    }
}
