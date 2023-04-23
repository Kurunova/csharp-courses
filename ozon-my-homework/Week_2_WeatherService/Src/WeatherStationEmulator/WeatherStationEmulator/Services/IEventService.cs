using WeatherStationEmulator.Common;

namespace WeatherStationEmulator.Services;

public interface IEventService
{
	EventResponse GenerateRandomResult();
}