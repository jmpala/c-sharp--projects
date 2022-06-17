using AdressBook.Model;

namespace AdressBook.Services;

public interface IAddressBookRepository
{
    IEnumerable<Contact> GetAll();

    Contact CreateNew(Contact newContact);

    void Delete(int id);

    List<Contact> FindByName(string nameFilter);
}