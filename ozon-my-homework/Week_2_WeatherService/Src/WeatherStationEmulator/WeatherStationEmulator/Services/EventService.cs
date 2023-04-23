using WeatherStationEmulator.Common;

namespace WeatherStationEmulator.Services;

public class EventService : IEventService
{	
	private readonly IEventStore _eventStore;

	public EventService(IEventStore eventStore)
	{
		_eventStore = eventStore;
	}
	
	public EventResponse GenerateRandomResult()
	{
		var random = new Random();
		var id = random.Next(0, 1000);

		if (_eventStore.TryGetEvent(id, out var result))
		{
			((EventResponse)result).State = ((EventResponse)result).State switch
			{
				State.Created => State.Updated,
				State.Updated => State.Deleted,
				_ => ((EventResponse)result).State
			};
		}
		else
		{
			var name = "Some string" + id;

			result = new EventResponse
			{
				Id = id,
				Name = name,
				State = State.Created,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};
			_eventStore.AddEvent(id, result);
		}

		return (EventResponse)result;
	}
}