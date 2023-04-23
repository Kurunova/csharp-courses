using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using WeatherStationEmulator.Common;

namespace WeatherStationEmulator.Services;

public class EventStore : IEventStore
{
	private readonly ConcurrentDictionary<long, IEvent> _events = new();

	public bool TryGetEvent(long id, [MaybeNullWhen(false)] out IEvent eventResponse)
	{
		return _events.TryGetValue(id, out eventResponse);
	}

	public void AddEvent(long id, IEvent eventResponse)
	{
		_events.AddOrUpdate(id, _ => eventResponse, (_, _) => eventResponse);
	}
}