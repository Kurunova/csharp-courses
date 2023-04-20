namespace B_SynchronizationContext;

public class BlockingQueue<T> : IDisposable
{
    private readonly object _syncLock = new object();
    private readonly Queue<T> _queue = new Queue<T>();
    private readonly SemaphoreSlim _pool = new SemaphoreSlim(0, int.MaxValue);

    public void Dispose()
    {
        _pool?.Dispose();
    }

    public void Enqueue(T item)
    {
        lock (_syncLock)
        {
            _queue.Enqueue(item);

            _pool.Release();
        }
    }

    public T Dequeue()
    {
        _pool.Wait();

        lock (_syncLock)
        {
            return _queue.Dequeue();
        }
    }

    public IEnumerable<T> ToArray()
    {
        lock (_syncLock)
        {
            return _queue.ToArray();
        }
    }
}