using System.ComponentModel.DataAnnotations;

namespace AdressBook.Model;

public class Contact
{
    public int ID { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}