using DemoLibrary.Models;

namespace DemoLibrary.DataAccess
{
    public interface IDataAccess
    {
        List<PersonModel> getPeople();
        PersonModel InsertPerson(string firstName, string lastName);
    }
}