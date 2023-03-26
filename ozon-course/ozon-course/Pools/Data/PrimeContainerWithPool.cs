namespace ozon_course.Pools.Data;

public sealed class PrimeContainerWithPool : IPoolableObject
{
	private static ISet<int> _primes = new HashSet<int>(10);

	// public PrimeContainerWithPool(PoolManager poolManager)
	// {
	// 	_poolManager = poolManager;
	// }
	//
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

	private PoolManager _poolManager;
	
	public void Dispose()
	{
		_poolManager.ReturnObject(this);
	}

	public void Reset()
	{
		_primes.Clear();
	}

	public void SetPoolManager(PoolManager poolManager)
	{
		_poolManager = poolManager;
	}
}