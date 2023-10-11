using System;  // Added this line
using DreamTravelWebAPI.Models;
using DreamTravelWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class TrainsController : ControllerBase
{
    private readonly ITrainService _trainService;

    public TrainsController(ITrainService trainService)
    {
        _trainService = trainService;
    }

    [HttpPost]
    public IActionResult Create(Train train)
    {
        try
        {
            if (string.IsNullOrEmpty(train.Id) || string.IsNullOrEmpty(train.Name))
            {
                return BadRequest("All fields are required.");
            }

            var result = _trainService.Create(train);
            return Ok("Successfully Created.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }


    [HttpPut("{id}")]
    public IActionResult Update(String id, Train updatedTrain)
    {
        try
        {
            _trainService.Update(id, updatedTrain);
            return Ok("Successfully Updated.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        try
        {
            _trainService.Delete(id);
            return Ok("Successfully Deleted.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }


    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var result = _trainService.GetAll();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpGet("published/{status}")]
    public IActionResult GetByIsPublished(bool status)
    {
        try
        {
            var result = _trainService.GetByIsPublished(status);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpPatch("{id}/deactivate")]
    public IActionResult DeactivateTrain(string id)
    {
        try
        {
            _trainService.DeactivateTrain(id);
            return Ok("Train de-activated successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }


    [HttpPatch("{trainId}/activate")]
    public IActionResult Activate(string trainId)
    {
        try
        {
            _trainService.ActiveTrain(trainId);
            return Ok("Train activated successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetById(String id)
    {
        try
        {
            var result = _trainService.GetById(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}
