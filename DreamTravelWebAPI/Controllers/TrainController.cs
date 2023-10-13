using System; // Namespace for fundamental classes and base classes that define commonly-used value and reference data types.
using DreamTravelWebAPI.Models; // Namespace containing the models used by the application.
using DreamTravelWebAPI.Services; // Namespace containing the services used by the application.
using Microsoft.AspNetCore.Mvc; // Namespace for ASP.NET Core MVC framework.

[Route("api/[controller]")] // Attribute-based routing. Maps to this controller when the URL matches "api/Trains".
[ApiController] // Attribute indicating this class is an API controller.
public class TrainsController : ControllerBase
{
    private readonly ITrainService _trainService; // Dependency Injection for the train service.

    // Constructor: Initializes a new instance of the TrainsController class.
    public TrainsController(ITrainService trainService)
    {
        _trainService = trainService; // Assigns the injected train service to the private variable.
    }

    [HttpPost] // Marks this method as a POST endpoint.
    public IActionResult Create(Train train)
    {
        try
        {
            // Validation for train details.
            if (string.IsNullOrEmpty(train.Id) || string.IsNullOrEmpty(train.Name))
            {
                return BadRequest("All fields are required.");
            }

            // Calls the Create method from the service and saves the train.
            var result = _trainService.Create(train);
            return Ok("Successfully Created.");
        }
        catch (Exception ex)
        {
            // Returns a 500 Internal Server Error response with the exception details.
            return StatusCode(500, new { error = "Cannot create successful.", details = ex.Message });
        }
    }

    [HttpPut("{id}")] // Marks this method as a PUT endpoint.
    public IActionResult Update(String id, Train updatedTrain)
    {
        try
        {
            // Calls the Update method from the service to update the train details.
            _trainService.Update(id, updatedTrain);
            return Ok("Successfully Updated.");
        }
        catch (Exception ex)
        {
            // Returns a 500 Internal Server Error response with the exception details.
            return StatusCode(500, new { error = "Update train by ID was unsuccessful.", details = ex.Message });
        }
    }

    // ... (The rest of the methods follow similar patterns)

    [HttpPatch("{trainId}/activate")] // Marks this method as a PATCH endpoint for train activation.
    public IActionResult Activate(string trainId)
    {
        try
        {
            // Calls the service method to activate the specified train.
            _trainService.ActiveTrain(trainId);
            return Ok("Train activated successfully.");
        }
        catch (Exception ex)
        {
            // Returns a 500 Internal Server Error response with the exception details.
            return StatusCode(500, new { error = "Train activation was unsuccessful.", details = ex.Message });
        }
    }

    [HttpGet("{id}")] // Marks this method as a GET endpoint to retrieve a train by its ID.
    public IActionResult GetById(String id)
    {
        try
        {
            var result = _trainService.GetById(id);
            if (result == null)
            {
                // If the train is not found, a 404 Not Found response is returned.
                return NotFound(new { message = "Train not found." });
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Returns a 500 Internal Server Error response with the exception details.
            return StatusCode(500, new { error = "Fetching train by ID was unsuccessful.", details = ex.Message });
        }
    }
}
