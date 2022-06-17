using AdressBook.Model;
using AdressBook.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdressBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IAddressBookRepository _repository;

    public ContactsController(IAddressBookRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Contact>))]
    public IActionResult GetAll() => Ok(_repository.GetAll());

    [HttpGet("findByName",Name = nameof(FindByName))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Contact>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult FindByName([FromQuery] string nameFilter = null)
    {
        if (string.IsNullOrEmpty(nameFilter)) return BadRequest("Invalid name to filter");
        return Ok(_repository.FindByName(nameFilter));
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Contact))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Post([FromBody] Contact contact)
    {
        if (contact.ID < 1 || string.IsNullOrEmpty(contact.Email)) return BadRequest("require fields missing");
        
        var createdContact = _repository.CreateNew(contact);
        return CreatedAtAction(nameof(FindByName), new { query = $"?nameFilter={contact.FirstName}" }, createdContact);
    }

    [HttpDelete("/{personId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteContact(int id)
    {
        if (id < 1) return BadRequest("Invalid person Id");
        try
        {
            _repository.Delete(id);
            return NoContent();
        }
        catch (Exception)
        {
            return NotFound("Client not found");
        }
    }
}