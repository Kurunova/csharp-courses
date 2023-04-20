using System.Text.Json;

namespace C_IAsyncEnumerable;

public static class Program
{
    public static async Task Main()
    {
        // IReadOnlyCollection<PublicApi> entries = await GetEntriesInfo();
        IAsyncEnumerable<PublicApi> entries = GetEntriesInfoAsync();

        await foreach (PublicApi publicApi in entries)
        {
            Console.WriteLine(publicApi.API);
            Console.WriteLine(publicApi.Description);
            Console.WriteLine(publicApi.Content.Substring(0, 300));

            Console.WriteLine(new string('*', 50));
        }
    }

    public static async Task<IReadOnlyCollection<PublicApi>> GetEntriesInfo()
    {
        IReadOnlyCollection<PublicApiDb> entries = await PublicApiRepository.GetEntries();

        var httpClient = new HttpClient();

        var list = new List<PublicApi>();

        foreach (PublicApiDb publicApiDb in entries)
        {
            try
            {
                string content = await httpClient.GetStringAsync(publicApiDb.Link);

                var publicApiContent = new PublicApi
                {
                    API = publicApiDb.API,
                    Content = content,
                    Description = publicApiDb.Description
                };

                list.Add(publicApiContent);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{publicApiDb.API} is not available: {e.Message}");
            }
        }

        return list;
    }

    public static async IAsyncEnumerable<PublicApi> GetEntriesInfoAsync()
    {
        IReadOnlyCollection<PublicApiDb> entries = await PublicApiRepository.GetEntries();

        var httpClient = new HttpClient();

        foreach (PublicApiDb publicApiDb in entries)
        {
            PublicApi publicApiContent;

            try
            {
                string content = await httpClient.GetStringAsync(publicApiDb.Link);

                publicApiContent = new PublicApi
                {
                    API = publicApiDb.API,
                    Content = content,
                    Description = publicApiDb.Description
                };
            }
            catch (Exception e)
            {
                Console.WriteLine($"{publicApiDb.API} is not available: {e.Message}");

                continue;
            }

            yield return publicApiContent;
        }
    }
}

public static class PublicApiRepository
{
    public static async Task<IReadOnlyCollection<PublicApiDb>> GetEntries()
    {
        await using FileStream fileStream = File.OpenRead("entries.txt");

        PublicApiDb[] result = await JsonSerializer.DeserializeAsync<PublicApiDb[]>(fileStream);

        return result;
    }
}

public class PublicApi
{
    public string API { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
}

public class PublicApiDb
{
    public string API { get; set; }
    public string Description { get; set; }
    public string Link { get; set; }
}