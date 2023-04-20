namespace E_Parallel;

internal static class ParallelForExample
{
    public static async Task Run()
    {
        var options = new ParallelOptions
        {
            MaxDegreeOfParallelism = 2
        };

        var from = 0;
        var to = 10;

        Parallel.For(from, to, options, i => SyncMethod(i));

        Parallel.For(from, to, options, async i => await AsyncAwaitMethod(i));

        Console.WriteLine("completed");
    }

    private static async Task<int> AsyncAwaitMethod(int index)
    {
        Console.WriteLine($"Async - {index} - start");

        await Task.Delay(TimeSpan.FromSeconds(1));

        Console.WriteLine($"Async {index} - end");

        return index;
    }

    private static int SyncMethod(int index)
    {
        Console.WriteLine($"Sync - {index} - start");

        Task.Delay(TimeSpan.FromSeconds(1)).Wait();

        Console.WriteLine($"Sync - {index} - end");

        return index;
    }
}