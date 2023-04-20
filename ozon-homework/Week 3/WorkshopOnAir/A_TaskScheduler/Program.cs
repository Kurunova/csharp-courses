namespace A_TaskScheduler;

public static class Program
{
    public static async Task Main()
    {
        var tts = new ThrottlerTaskScheduler(3);

        var tasks = new List<Task>();

        for (var i = 0; i < 100; i++)
        {
            int local = i;

            Task actionTask = tts.Run(
                async () =>
                {
                    // Console.WriteLine($"#{local} - start on thread {Environment.CurrentManagedThreadId}");

                    await Task.Delay(TimeSpan.FromSeconds(10));

                    // Console.WriteLine($"#{local} - stop on thread {Environment.CurrentManagedThreadId}");
                });

            tasks.Add(actionTask);
        }

        await Task.WhenAll(tasks);
    }

    public class ThrottlerTaskScheduler : TaskScheduler, IDisposable
    {
        private readonly BlockingQueue<Task> _tasksQueue = new BlockingQueue<Task>();
        private readonly Task[] _workers;

        public ThrottlerTaskScheduler(int workersCount)
        {
            _workers = new Task[workersCount];

            for (var i = 0; i < workersCount; i++)
            {
                _workers[i] = Task.Factory.StartNew(TryExecuteTasks, TaskCreationOptions.LongRunning);
            }
        }

        public void Dispose()
        {
            _tasksQueue.Dispose();
        }

        public Task Run(Action action)
        {
            var tcs = new CancellationTokenSource();

            tcs.CancelAfter(TimeSpan.FromSeconds(5));

            CancellationToken token = tcs.Token;

            Task actionTask = Task.Factory.StartNew(action, token, TaskCreationOptions.None, this);

            return actionTask;
        }

        private void TryExecuteTasks()
        {
            while (true)
            {
                Task task = _tasksQueue.Dequeue();

                TryExecuteTask(task);
            }
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return _tasksQueue.ToArray();
        }

        protected override void QueueTask(Task task)
        {
            _tasksQueue.Enqueue(task);
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            return false;
        }
    }
}