using DreamTravelWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/trains/{trainId}/[controller]")]
[ApiController]
public class SchedulesController : ControllerBase
{
    private readonly IScheduleService _scheduleService;

    public SchedulesController(IScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    [HttpGet]
    public IActionResult GetAllByTrainId(int trainId)
    {
        var schedules = _scheduleService.GetByTrainId(trainId);
        return Ok(schedules);
    }

    [HttpPost]
    public IActionResult Create(int trainId, Schedule schedule)
    {
        var newSchedule = _scheduleService.Create(trainId, schedule);
        return Ok(newSchedule);
    }
    // Additional CRUD methods for schedules can be added...
}
