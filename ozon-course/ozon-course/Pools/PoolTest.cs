using BenchmarkDotNet.Attributes;
using ozon_course.Pools.Data;

namespace ozon_course.Pools;

public class PoolTest
{
	private PoolManager _poolManager = new PoolManager();

	[GlobalSetup]
	public void GlobalSetup()
	{
		for (int i = 0; i < 3; i++)
		{
			var primeContainerWithPool = new PrimeContainerWithPool();
			primeContainerWithPool.SetPoolManager(_poolManager);
			_poolManager.ReturnObject(primeContainerWithPool);
		}
	}

	[Benchmark]
	public List<int[]> ProcessWithPool()
	{
		var result = new List<int[]>();

		for (int i = 0; i < 100; i++)
		{
			using var container = _poolManager.GetObject<PrimeContainerWithPool>();
			
			for (int j = 0; j < 50; j++)
			{
				container.TryAdd(j);
			}
				
			result.Add(container.Result);
		}
		
		return result;
	}
	
	[Benchmark]
	public List<int[]> Process()
	{
		var result = new List<int[]>();

		for (int i = 0; i < 100; i++)
		{
			var container = new PrimeContainer();
			
			for (int j = 0; j < 50; j++)
			{
				container.TryAdd(j);
			}
				
			result.Add(container.Result);
		}
		
		return result;
	}
}