using System.Diagnostics.CodeAnalysis;
using WeatherStationEmulator.Common;

namespace WeatherStationEmulator.Services;

public interface IEventStore
{
	bool TryGetEvent(long id, [MaybeNullWhen(false)] out IEvent eventResponse);

	void AddEvent(long id, IEvent eventResponse);
}