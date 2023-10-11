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
            var result = _trainService.Create(train);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPut("{id}")]
    public IActionResult Update(String id, Train updatedTrain)
    {
        try
        {
            _trainService.Update(id, updatedTrain);
            return Ok();  // Just return an Ok without a result since Update is void
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

[HttpDelete("{id}")]
public IActionResult Delete(string id)
{
    try
    {
        _trainService.Delete(id);
        return Ok();
    }
    catch (Exception ex)
    {
        return BadRequest(ex.Message);
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
            return BadRequest(ex.Message);
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
            return BadRequest(ex.Message);
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
            return BadRequest(ex.Message);
        }
    }
}
