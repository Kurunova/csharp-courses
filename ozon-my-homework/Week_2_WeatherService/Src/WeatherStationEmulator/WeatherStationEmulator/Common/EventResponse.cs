namespace WeatherStationEmulator.Common;

public class EventResponse : IEvent
{
	public long Id { get; set; }

	public string Name { get; set; }

	public State State { get; set; }

	public DateTime CreatedAt { get; set; }

	public DateTime UpdatedAt { get; set; }
}