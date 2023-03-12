using BenchmarkDotNet.Attributes;

namespace ozon_course.LinqExample;

public class User
{
	public int Age;
}

[MemoryDiagnoser()]
public class ExampleLinq
{
	private List<User> _users = new List<User>();

	[GlobalSetup]
	public void GlobalSetup()
	{
		for (int i = 0; i < 20; i++)
		{
			_users.Add(new User());
		}
	}

	[Benchmark()]
	public int ForeachExample()
	{
		var sum = 0;

		foreach (var user in _users)
		{
			sum += user.Age;
		}

		return sum / _users.Count;
	}
	
	[Benchmark()]
	public int ForExample()
	{
		var sum = 0;

		for (var i = 0; i < _users.Count; i++)
		{
			sum += _users[i].Age;
		}

		return sum / _users.Count;
	}
	
	[Benchmark()]
	public int LinqExample()
	{
		var sum = _users.Sum(t => t.Age);

		return sum / _users.Count;
	}
	
		
	[Benchmark()]
	public int Linq2Example()
	{
		var sum = _users.Select(t => t.Age).Sum();

		return sum / _users.Count;
	}
}