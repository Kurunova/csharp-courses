using Google.Protobuf.WellKnownTypes;
using WeatherStationEmulator.GrpcServices;
using State = WeatherStationEmulator.Common.State;

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
				GrpcServices.State.Created => GrpcServices.State.Updated,
				GrpcServices.State.Updated => GrpcServices.State.Deleted,
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
				State = GrpcServices.State.Created,
				CreatedAt = DateTime.UtcNow.ToTimestamp(),
				UpdatedAt = DateTime.UtcNow.ToTimestamp()
			};
			_eventStore.AddEvent(id, result);
		}

		return (EventResponse)result;
	}
}