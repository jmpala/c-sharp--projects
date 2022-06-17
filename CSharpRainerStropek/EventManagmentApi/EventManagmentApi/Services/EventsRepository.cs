namespace EventManagmentApi.Services;

public record Event(int Id, DateTime Date, string Location, string Description);

public class EventsRepository : IEventsRepository
{
    private List<Event> Events { get; } = new();

    public Event Add(Event e)
    {
        Events.Add(e);
        return e;
    }

    public IEnumerable<Event> GetAll() => Events;

    public Event? GetById(int id) => Events.FirstOrDefault(e => e.Id == id);

    public void Delete(int id)
    {
        var e = GetById(id);
        if (e == null) throw new ArgumentException("No event exist with the given id", nameof(id));

        Events.Remove(e);
    }
}