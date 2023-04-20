namespace A_TaskScheduler;

public class BlockingQueue<T> : IDisposable
{
    private readonly object _syncLock = new object();
    private readonly Queue<T> _queue = new Queue<T>();
    private readonly SemaphoreSlim _pool = new SemaphoreSlim(0, int.MaxValue);

    private int AvailableTasks = 0;

    public void Dispose()
    {
        _pool?.Dispose();
    }

    public void Enqueue(T item)
    {
        lock (_syncLock)
        {
            _queue.Enqueue(item);

            AvailableTasks++;

            _pool.Release();

            Console.WriteLine($"enqueue: avaiable tasks in queue - {AvailableTasks}, semaphore - {_pool.CurrentCount} ");
        }
    }

    public T Dequeue()
    {
        _pool.Wait();

        lock (_syncLock)
        {
            AvailableTasks--;

            Console.WriteLine($"dequeue: avaiable tasks in queue - {AvailableTasks}, semaphore - {_pool.CurrentCount} ");

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