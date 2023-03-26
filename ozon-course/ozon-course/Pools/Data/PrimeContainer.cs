namespace ozon_course.Pools.Data;

public class PrimeContainer
{
	private static ISet<int> _primes = new HashSet<int>(10);

	public void TryAdd(int value)
	{
		if (value < 1)
		{
			return;
		}

		if (value == 1)
		{
			_primes.Add(value);
		}

		for (int i = 2; i < value; i++)
		{
			if (value % i == 0)
			{
				return;
			}
		}

		_primes.Add(value);
	}

	public int[] Result = _primes.ToArray();
}