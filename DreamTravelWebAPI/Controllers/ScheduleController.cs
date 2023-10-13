using DreamTravelWebAPI.Models; // Imports the models used by the application.
using Microsoft.AspNetCore.Mvc; // Imports the ASP.NET Core MVC framework.
using System; // Imports the System namespace for basic utility classes.
using System.Collections.Generic; // Imports collections that should be used throughout the application.

namespace DreamTravelWebAPI.Controllers
{
    [Route("api/schedules")] // Sets the base route for this controller to "api/schedules".
    [ApiController] // Indicates that this class is an API controller.
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleService _scheduleService; // Dependency injection for the schedule service.

        // Constructor initializes the SchedulesController and injects the schedule service.
        public SchedulesController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpPost("{trainId}")] // Endpoint to create a schedule with train details.
        public IActionResult CreateScheduleWithTrainDetails(string trainId, [FromBody] Schedule schedule)
        {
            try
            {
                // Creates a new schedule with train details.
                var createdSchedule = _scheduleService.CreateScheduleWithTrainDetails(trainId, schedule);
                // Returns the created schedule.
                return CreatedAtAction(nameof(GetScheduleById), new { scheduleId = createdSchedule.Id }, createdSchedule);
            }
            catch (Exception ex)
            {
                // Returns an error if something goes wrong.
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{scheduleId}")] // Endpoint to get a schedule by its ID.
        public IActionResult GetScheduleById(int scheduleId)
        {
            try
            {
                // Fetches the schedule based on its ID.
                var schedule = _scheduleService.GetScheduleById(scheduleId);
                if (schedule == null)
                {
                    return NotFound("Schedule not found."); // Returns a not found status if the schedule doesn't exist.
                }
                return Ok(schedule); // Returns the fetched schedule.
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Handles potential exceptions.
            }
        }

        // ... (Similar comments apply for the remaining methods)

        [HttpGet("published-trains")] // Endpoint to get schedules of published trains.
        public IActionResult GetSchedulesOfPublishedTrains()
        {
            try
            {
                // Fetches the schedules of all published trains.
                var schedules = _scheduleService.GetSchedulesOfPublishedTrains();
                return Ok(schedules); // Returns the fetched schedules.
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Handles potential exceptions.
            }
        }
    }
}
