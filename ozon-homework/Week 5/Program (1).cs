using System.Globalization;
using System.Text.Json;

using Confluent.Kafka;

namespace A_Kafka;

public static class Program
{
    public static string KafkaHost = "localhost:9092";
    public static string PersonTopic = "person_event";

    public class CustomKafkaSerializer<T> : ISerializer<T>, IDeserializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            return JsonSerializer.SerializeToUtf8Bytes(data);
        }

        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            return JsonSerializer.Deserialize<T>(data);
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public static async Task Main()
    {
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = KafkaHost,
            GroupId = "test_group_1",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        };

        Task consumer1 = Task.Run(async () => await ConsumeMessages(1, consumerConfig, CancellationToken.None));

        await consumer1;

        Console.ReadLine();
    }

    public static async Task ConsumeMessages(int consumerNumber, ConsumerConfig consumerConfig, CancellationToken ct)
    {
        IConsumer<string, Person> consumer = new ConsumerBuilder<string, Person>(consumerConfig)
            .SetValueDeserializer(new CustomKafkaSerializer<Person>())
            .Build();

        consumer.Subscribe(new[] { PersonTopic });

        var lastProcessedIsSuccessfully = true;

        while (!ct.IsCancellationRequested)
        {
            ConsumeResult<string, Person> msg = null;

            if (lastProcessedIsSuccessfully)
            {
                msg = consumer.Consume(TimeSpan.FromMilliseconds(100));
            }

            try
            {
                if (msg != null)
                {
                    ProcessMessage(msg.Message.Value);

                    consumer.Commit(msg);
                    lastProcessedIsSuccessfully = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"processing error ({msg.Topic}:{msg.Partition}:{msg.Offset}): {e.Message}");

                lastProcessedIsSuccessfully = false;
            }

            await Task.Delay(TimeSpan.FromMilliseconds(300), ct);
        }
    }

    private static void ProcessMessage<TKey, TValue>(IReadOnlyCollection<Message<TKey, TValue>> message)
    {
    }

    private static void ProcessMessages(T[] messages)
    {
        // batch processing
    }

    public static void ProduceMessages()
    {
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = KafkaHost
        };

        using IProducer<string, Person> producer = new ProducerBuilder<string, Person>(producerConfig)
            .SetValueSerializer(new CustomKafkaSerializer<Person>())
            .Build();

        var rand = new Random();

        for (var i = 0; i < 100; i++)
        {
            var msg = new Message<string, Person>
            {
                Key = i.ToString(),
                Value = new Person
                {
                    Name = Guid.NewGuid().ToString(),
                    Age = rand.Next(20, 50)
                }
            };

            producer.Produce(PersonTopic, msg);
        }

        producer.Flush();
    }
}