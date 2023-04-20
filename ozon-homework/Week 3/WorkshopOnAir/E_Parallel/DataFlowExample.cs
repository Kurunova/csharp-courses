using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks.Dataflow;

using E_Parallel.Contracts;

namespace E_Parallel;

public static class DataFlowExample
{
    public static async Task Run()
    {
        var linkOptions = new DataflowLinkOptions
        {
            PropagateCompletion = true
        };

        var writeRawMessagesBlock = new TransformManyBlock<JsonArray, string>(usersJson => { return usersJson.Select(x => x.ToJsonString()); });
        var deserializeUserBlock = new TransformBlock<string, Result>(async userJson => { return JsonSerializer.Deserialize<Result>(userJson)!; });
        var processingBlock = new BroadcastBlock<Result>(i => i);
        var downloadImgBlock = new TransformBlock<Result, UserPicture>(
            async item =>
            {
                byte[] img = await new HttpClient().GetByteArrayAsync(item.picture.medium);

                return new UserPicture
                {
                    Login = item.login.username,
                    Bytes = img
                };
            });
        var saveUserImgBlock = new ActionBlock<UserPicture>(item => { File.WriteAllBytes($@"d:\Dev\workshop\dataflow\{item.Login}.jpg", item.Bytes); });
        var saveUserJsonBlock = new ActionBlock<Result>(
            item =>
            {
                string json = JsonSerializer.Serialize(item, new JsonSerializerOptions { WriteIndented = true });

                File.WriteAllText($@"d:\Dev\workshop\dataflow\{item.login.username}.json", json);
            });

        writeRawMessagesBlock.LinkTo(deserializeUserBlock, linkOptions);
        deserializeUserBlock.LinkTo(processingBlock, linkOptions);

        processingBlock.LinkTo(downloadImgBlock, linkOptions);
        processingBlock.LinkTo(saveUserJsonBlock, linkOptions);

        downloadImgBlock.LinkTo(saveUserImgBlock, linkOptions);

        Task start = UserProducer.Start(writeRawMessagesBlock);

        await Task.WhenAll(saveUserJsonBlock.Completion, saveUserImgBlock.Completion);
    }

    public static class UserProducer
    {
        public static async Task Start(TransformManyBlock<JsonArray, string> targetBlock)
        {
            IAsyncEnumerable<JsonArray> userStream = UserRepository.GetRawUsersInfo();

            await foreach (JsonArray userJson in userStream)
            {
                await targetBlock.SendAsync(userJson);
            }

            targetBlock.Complete();
        }
    }

    public static class UserRepository
    {
        public static async IAsyncEnumerable<JsonArray> GetRawUsersInfo()
        {
            var httpClient = new HttpClient();

            for (var i = 0; i < 10; i++)
            {
                string users = await httpClient.GetStringAsync("https://randomuser.me/api/?results=10");

                JsonObject jsonObject = JsonNode.Parse(users).AsObject();

                yield return jsonObject["results"].AsArray();
            }
        }
    }
}