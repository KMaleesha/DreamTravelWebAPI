using DreamTravelWebAPI.Models;
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
    public IActionResult Update(int id, Train updatedTrain)
    {
        try
        {
            var result = _trainService.Update(id, updatedTrain);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            var result = _trainService.Delete(id);
            return Ok(result);
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

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
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
