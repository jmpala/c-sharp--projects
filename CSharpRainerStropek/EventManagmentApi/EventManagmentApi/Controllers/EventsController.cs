using EventManagmentApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventManagmentApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly IEventsRepository _repository;

    public EventsController(IEventsRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Event>))] // informs asp.net core return code and type
    public IActionResult GetAll() => Ok(_repository.GetAll());

    [HttpGet("{id}", Name = nameof(GetById))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Event))]
    public IActionResult GetById(int id)
    {
        var e = _repository.GetById(id);
        if (e == null) return NotFound();
        return Ok(e);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Event))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Add([FromBody] Event newEvent)
    {
        if (newEvent.Id < 1) return BadRequest("Invalid id");
        
        _repository.Add(newEvent);
        return CreatedAtAction(nameof(GetById), new { id = newEvent.Id } ,newEvent);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(int id)
    {
        try
        {
            _repository.Delete(id);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }

        return NoContent();
    }
}