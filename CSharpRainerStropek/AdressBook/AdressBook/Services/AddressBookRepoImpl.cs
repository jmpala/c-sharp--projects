using System.Globalization;
using AdressBook.Model;

namespace AdressBook.Services;

public class AddressBookServiceImpl : IAddressBookRepository
{
    public List<Contact> Contacts { get; } = new();

    public IEnumerable<Contact> GetAll() => Contacts;

    public Contact CreateNew(Contact newContact)
    {
        Contacts.Add(newContact);
        return newContact;
    }

    public void Delete(int id)
    {
        var contact = Contacts.FirstOrDefault(c => c.ID == id);
        if (contact == null) throw new Exception("Contact not found");
        Contacts.Remove(contact);
    }

    public List<Contact> FindByName(string nameFilter)
    {
        if (string.IsNullOrEmpty(nameFilter)) throw new ArgumentException("Null or Empty parameter");
        return Contacts
            .Where(c => c.FirstName.Contains(nameFilter) || c.LastName.Contains(nameFilter))
            .ToList();
    } 
}