namespace B_SynchronizationContext;

public static class Program
{
    public static async Task Main()
    {
        Console.WriteLine($"{Environment.CurrentManagedThreadId} - program start");

        var syncContext = new ThrottlingSynchronizationContext();
        SynchronizationContext.SetSynchronizationContext(syncContext);

        var contextWrapper = new ThrottlerContextWrapper(3, syncContext);

        var httpClient = new HttpClient();

        string response1 = await httpClient.GetStringAsync("https://ozon.ru");

        Console.WriteLine($"{Environment.CurrentManagedThreadId} - {1} - request complete {response1.Length}");

        string response2 = await httpClient.GetStringAsync("https://ozon.ru");

        Console.WriteLine($"{Environment.CurrentManagedThreadId} - {2} - request complete {response2.Length}");

        // var list = new List<Task>();
        //
        // for (var i = 0; i < 30; i++)
        // {
        //     list.Add(Test(i));
        // }

        // await Task.WhenAll(list);

        // Task t1 = Test();
        // Task t2 = Test();

        Console.ReadLine();

        // Task.WhenAll(t1, t2).Wait();
    }

    public static async Task Test(int i)
    {
        var httpClient = new HttpClient();

        string response = await httpClient.GetStringAsync("https://ozon.ru");

        Console.WriteLine($"{Environment.CurrentManagedThreadId} - {i} - request complete {response.Length}");
    }
}

public class ThrottlerContextWrapper
{
    public ThrottlingSynchronizationContext SyncContext { get; private set; }

    public ThrottlerContextWrapper(int workersCount, ThrottlingSynchronizationContext synchronizationContext)
    {
        SyncContext = synchronizationContext;

        var tasks = new List<Task>();

        for (var i = 0; i < workersCount; i++)
        {
            tasks.Add(
                Task.Factory.StartNew(
                    StartQueueProcessing,
                    CancellationToken.None,
                    TaskCreationOptions.LongRunning,
                    TaskScheduler.Current));
        }
    }

    private void StartQueueProcessing()
    {
        // SynchronizationContext.SetSynchronizationContext(SyncContext);

        while (true)
        {
            SendOrPostCallbackItem callback = SyncContext.Dequeue();

            Console.WriteLine($"{Environment.CurrentManagedThreadId} - EXECUTE call before");
            callback.Execute();
            Console.WriteLine($"{Environment.CurrentManagedThreadId} - EXECUTE call after");
        }
    }
}

public class ThrottlingSynchronizationContext : SynchronizationContext
{
    private readonly BlockingQueue<SendOrPostCallbackItem> _callbackItemsQueue = new BlockingQueue<SendOrPostCallbackItem>();

    public override SynchronizationContext CreateCopy()
    {
        return this;
    }

    public override void Post(SendOrPostCallback d, object state)
    {
        // Console.WriteLine($"{Environment.CurrentManagedThreadId} - POST call - added to queue");

        _callbackItemsQueue.Enqueue(
            new SendOrPostCallbackItem
            {
                Callback = d,
                State = state
            });
    }

    public SendOrPostCallbackItem Dequeue()
    {
        SendOrPostCallbackItem item = _callbackItemsQueue.Dequeue();

        return item;
    }

    public override void Send(SendOrPostCallback d, object? state)
    {
        // Console.WriteLine("SEND !!!");

        d(state);
    }
}