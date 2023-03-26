namespace ozon_course.Pools.Data;

public sealed class PoolManager
{
	private class Pool
	{
		public int PoolSize { get; set; }
		public Stack<IPoolableObject> Stack { get; private set; }
		public int Count => Stack.Count;

		public Pool()
		{
			Stack = new Stack<IPoolableObject>();
		}
	}

	private const int MaxSizePerType = 100;
	
	private Dictionary<Type, Pool> pools = new Dictionary<Type, Pool>();

	public T GetObject<T>() where T : class, IPoolableObject, new()
	{
		Pool pool;

		T valueToReturn = null;

		if (pools.TryGetValue(typeof(T), out pool))
		{
			if (pool.Stack.Count > 0)
			{
				valueToReturn = pool.Stack.Pop() as T;
			}
		}
		else
		{
			pool = new Pool();
			pools[typeof(T)] = pool;
		}

		if (valueToReturn == null)
		{
			valueToReturn = new T();
		}

		valueToReturn.SetPoolManager(this);

		if (pool.PoolSize > 0)
		{
			pool.PoolSize--;
		}

		return valueToReturn;
	}

	public void ReturnObject<T>(T value) where T : class, IPoolableObject, new()
	{
		Pool pool;

		if (pools.TryGetValue(typeof(T), out pool))
		{
			pool = new Pool();
			pools[typeof(T)] = pool;
		}

		if (pool.PoolSize + 1 <= MaxSizePerType)
		{
			pool.PoolSize++;
			value.Reset();
			pool.Stack.Push(value);
		}
	}
}