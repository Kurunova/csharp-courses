namespace ozon_course.LargeClasses;

public class SimpleUserOld
{
	public int Id { get; init; }

	public DateTime FirstWorkTime { get; init; }
	public DateTime LastWorkTime { get; init; }
	public TimeSpan AvgWorkTime { get; init; }
	public TimeSpan MedianWorkTime { get; init; }
	public TimeSpan MinWorkTime { get; init; }
	public TimeSpan MaxWorkTime { get; init; }
	
	public int CurrentSalary { get; init; }
	public int FirstSalary { get; init; }
	public int MinSalary { get; init; }
	public int MaxSalary { get; init; }
	public int AvgSalary { get; init; }
	public int MedianSalary { get; init; }
}