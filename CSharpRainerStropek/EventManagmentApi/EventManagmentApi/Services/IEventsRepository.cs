namespace EventManagmentApi.Services;

public interface IEventsRepository
{
    Event Add(Event e);

    IEnumerable<Event> GetAll();

    Event? GetById(int id);

    void Delete(int id);
}