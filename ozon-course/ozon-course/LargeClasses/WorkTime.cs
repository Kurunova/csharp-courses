namespace ozon_course.LargeClasses;

public class WorkTime
{
	public DateTime FirstWorkTime { get; init; }
	public DateTime LastWorkTime { get; init; }
	public TimeSpan AvgWorkTime { get; init; }
	public TimeSpan MedianWorkTime { get; init; }
	public TimeSpan MinWorkTime { get; init; }
	public TimeSpan MaxWorkTime { get; init; }
}