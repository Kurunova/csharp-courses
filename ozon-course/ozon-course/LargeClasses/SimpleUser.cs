namespace ozon_course.LargeClasses;

public class SimpleUser
{
	public SimpleUser(int id, WorkTime workTime, Salary salary)
	{
		Id = id;
		WorkTime = workTime;
		Salary = salary;
	}
	
	public int Id { get; init; }

	public WorkTime WorkTime { get; init; }

	public Salary Salary { get; init; }
}