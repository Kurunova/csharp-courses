using Microsoft.AspNetCore.Mvc;
using WeatherStationEmulator.Common;
using WeatherStationEmulator.Services;

namespace WeatherStationEmulator.Controllers;

[Route("events")]
public class EventsController : ControllerBase
{
	private readonly IEventService _eventService;

	public EventsController(IEventService eventService)
	{
		_eventService = eventService;
	}

	[HttpGet]
	public Task<EventResponse> GetEvent()
	{
		var result = _eventService.GenerateRandomResult();

		return Task.FromResult(result);
	}
}