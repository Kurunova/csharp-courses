using WeatherStationEmulator.Common;
using WeatherStationEmulator.GrpcServices;

namespace WeatherStationEmulator.Services;

public interface IEventService
{
	EventResponse GenerateRandomResult();
}