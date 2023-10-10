using DreamTravelWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DreamTravelWebAPI.Controllers
{
    [Route("api/schedules")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public SchedulesController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpPost("{trainId}")]
        public IActionResult CreateScheduleWithTrainDetails(string trainId, [FromBody] Schedule schedule)
        {
            try
            {
                var createdSchedule = _scheduleService.CreateScheduleWithTrainDetails(trainId, schedule);
                return CreatedAtAction(nameof(GetScheduleById), new { scheduleId = createdSchedule.Id }, createdSchedule);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{scheduleId}")]
        public IActionResult GetScheduleById(int scheduleId)
        {
            try
            {
                var schedule = _scheduleService.GetScheduleById(scheduleId);
                if (schedule == null)
                {
                    return NotFound("Schedule not found.");
                }
                return Ok(schedule);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("train/{trainId}")]
        public IActionResult GetSchedulesByTrainId(String trainId)
        {
            try
            {
                var schedules = _scheduleService.GetSchedulesByTrainId(trainId);
                return Ok(schedules);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{scheduleId}")]
        public IActionResult UpdateExistingTrainSchedule(int scheduleId, Schedule updatedSchedule)
        {
            try
            {
                var isUpdated = _scheduleService.UpdateExistingTrainSchedule(scheduleId, updatedSchedule);
                if (!isUpdated)
                {
                    return NotFound("Schedule not found or update failed.");
                }
                return Ok("Schedule updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllSchedules()
        {
            try
            {
                var schedules = _scheduleService.GetAllSchedules();
                return Ok(schedules);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{scheduleId}")]
        public IActionResult CancelTrainReservation(int scheduleId)
        {
            try
            {
                var isCancelled = _scheduleService.CancelTrainReservation(scheduleId);
                if (!isCancelled)
                {
                    return NotFound("Schedule not found or cancellation failed.");
                }
                return Ok("Train reservation cancelled successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
