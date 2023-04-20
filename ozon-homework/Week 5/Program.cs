using Google.Protobuf;

using StackExchange.Redis;

namespace B_Redis;

public static class Program
{
    public static async Task Main()
    {
        ConnectionMultiplexer redis = await ConnectionMultiplexer.ConnectAsync("localhost");

        IDatabase db = redis.GetDatabase();

        var user = new Person
        {
            Id = int.MaxValue,
            FirstName = Guid.NewGuid().ToString(),
            LastName = Guid.NewGuid().ToString()
        };

        Console.WriteLine(user.ToString());

        var jsonString = user.ToString();
        byte[] byteArray = user.ToByteArray();

        // TTL
        db.StringSet("person_string", jsonString, TimeSpan.FromMilliseconds(10));
        db.StringSet("person_byte", byteArray);

        // event-based
        // producer.Publish(new PersonUpdateEvent { personID: 123, changedFields: [] } )
        RedisValue personBytes = db.StringGet("person_byte");
        Person person1 = Person.Parser.ParseFrom(personBytes);
        db.StringSet("person_byte", person1.ToByteArray());

        db.HashSet(
            "person_hash",
            new[]
            {
                new HashEntry("id", int.MaxValue),
                new HashEntry("FirstName", Guid.NewGuid().ToString()),
                new HashEntry("LastName", Guid.NewGuid().ToString())
            });
    }
}