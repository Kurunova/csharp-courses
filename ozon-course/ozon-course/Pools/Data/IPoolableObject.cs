namespace ozon_course.Pools.Data;

public interface IPoolableObject : IDisposable
{
	void Reset(); // для того чтобы возвращать объект в пул
	void SetPoolManager(PoolManager poolManager);
}