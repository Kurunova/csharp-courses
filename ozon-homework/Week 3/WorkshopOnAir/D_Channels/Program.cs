using System.Net.Http.Json;
using System.Threading.Channels;

namespace D_Channels;

public static class Program
{
    public static async Task Main()
    {
        var channel = Channel.CreateUnbounded<Name>();

        Task producer = Task.Run(async () => { await NameProducer.ProduceMessages(channel.Writer); });

        Task consumer1 = Task.Run(async () => { await NameConsumer.Enrich(channel.Reader); });

        // Task consumer2 = Task.Run(async () => { await NameConsumer.Enrich(channel.Reader); });
        // Task consumer3 = Task.Run(async () => { await NameConsumer.Enrich(channel.Reader); });

        await Task.WhenAll(producer, consumer1);

        // await Task.WhenAll(producer, consumer1, consumer2, consumer3);
    }

    public static class UserRepository
    {
        public static async IAsyncEnumerable<Name> GetNames()
        {
            var httpClient = new HttpClient();

            for (var i = 0; i < 10; i++)
            {
                var names = await httpClient.GetFromJsonAsync<RootObject>("https://randomuser.me/api/?inc=name&results=10");

                foreach (Result name in names.results)
                {
                    yield return name.name;
                }
            }
        }
    }

    public static class NameProducer
    {
        public static async Task ProduceMessages(ChannelWriter<Name> channelWriter)
        {
            await foreach (Name name in UserRepository.GetNames())
            {
                await channelWriter.WriteAsync(name);
            }

            channelWriter.Complete();
        }
    }

    public static class NameConsumer
    {
        public static async Task Enrich(ChannelReader<Name> channelReader)
        {
            try
            {
                while (true)
                {
                    Name item = await channelReader.ReadAsync();

                    Console.WriteLine($"{item.first} {item.last}");

                    await Task.Delay(1000);
                }

                // await foreach (Name item in channelReader.ReadAllAsync())
                // {
                // }
            }
            catch (ChannelClosedException)
            {
                Console.WriteLine("Channel was closed!");
            }
        }
    }
}