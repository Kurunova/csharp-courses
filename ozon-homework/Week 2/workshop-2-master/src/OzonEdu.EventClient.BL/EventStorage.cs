using System.Collections.Concurrent;

namespace OzonEdu.EventClient.BL;

public class EventStorage : IEventStorage
{
    private readonly ConcurrentDictionary<long, Event> _events = new();
   
    public void AddEvent(Event @event)
    {
        _events.AddOrUpdate(@event.Id, _ => @event, (_, _) => @event);
    }

    public void RemoveEvent(int id)
    {
        _events.Remove(id, out _);
    }

    public Event GetById(int id)
    {
        if (_events.TryGetValue(id, out var @event))
        {
            return @event;
        }

        return null;
    }

    public int GetCountEventsByState(StateType stateType)
    {
        return _events.Values.Count(x => x.State == stateType);
    }
}